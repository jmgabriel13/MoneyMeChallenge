using Application.Interfaces;
using Domain.Errors;
using Domain.Shared;

namespace Application.Customers.UpdateFinanceDetails;
internal sealed class UpdateFinanceDetailsHandler(
    ICustomerRepository _customerRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<UpdateFinanceDetailsCommand>
{
    public async Task<Result> Handle(UpdateFinanceDetailsCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            // Return error, cant proceed if customer or product are not found
            return Result.Failure(DomainErrors.Customer.CustomerNotFound(request.CustomerId));
        }

        customer.Loan.Update(request.TermInMonths, request.AmountRequired);

        _customerRepository.Update(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
