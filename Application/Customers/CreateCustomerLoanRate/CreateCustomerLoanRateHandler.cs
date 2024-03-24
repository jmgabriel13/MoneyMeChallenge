using Application.Interfaces;
using Domain.Entities.Customers;
using Domain.Errors;
using Domain.Shared;

namespace Application.Customers.CreateCustomerLoanRate;
internal sealed class CreateCustomerLoanRateHandler(
    ICustomerRepository _customerRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<CreateCustomerLoanRateCommand, string>
{
    public async Task<Result<string>> Handle(CreateCustomerLoanRateCommand request, CancellationToken cancellationToken)
    {
        bool isValidDate = DateTime.TryParse(request.DateOfBirth, out DateTime parsedDate);
        if (!isValidDate)
        {
            return Result.Failure<string>(DomainErrors.Customer.InvalidDateOfBirth);
        }

        // Check if firstName, lastName and dateOfBirth is existing, then return same redirect URL
        var customer = await _customerRepository.GetByFirstLastNameAndDateOfBirthAsync(
            request.FirstName,
            request.LastName,
            parsedDate,
            cancellationToken);

        if (customer is not null)
        {
            // If customer is existing using firstName, lastName and dateOfbirth, return same redirectUrl
            return Result.Success(customer.RedirectURL);
        }

        // Creating new customer with loan
        var newCustomer = Customer.Create(
            Guid.NewGuid(),
            request.Title,
            request.FirstName,
            request.LastName,
            parsedDate,
            request.MobileNumber,
            request.Email,
            request.redirectUrl,
            request.Term,
            request.AmountRequired);

        _customerRepository.Add(newCustomer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(newCustomer.RedirectURL);
    }
}
