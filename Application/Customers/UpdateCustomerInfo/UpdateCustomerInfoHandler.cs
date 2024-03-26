using Application.Interfaces;
using Domain.Errors;
using Domain.Shared;

namespace Application.Customers.UpdateCustomerInfo;
internal sealed class UpdateCustomerInfoHandler(
    ICustomerRepository _customerRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<UpdateCustomerInfoCommand>
{
    public async Task<Result> Handle(UpdateCustomerInfoCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            // Return error, cant proceed if customer or product are not found
            return Result.Failure(DomainErrors.Customer.CustomerNotFound(request.CustomerId));
        }

        customer.Update(request.FirstName, request.LastName, request.Mobile, request.Email);

        _customerRepository.Update(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
