using Application.Interfaces;
using Domain.Errors;
using Domain.Shared;

namespace Application.Customers.GetCustomerLoanById;
internal sealed class GetCustomerLoanByIdHandler(
    ICustomerRepository _customerRepository) : IQueryHandler<GetCustomerLoanByIdQuery, CustomerLoanResponse>
{
    public async Task<Result<CustomerLoanResponse>> Handle(GetCustomerLoanByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindByIdAsync(request.customerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<CustomerLoanResponse>(DomainErrors.Customer.CustomerNotFound(request.customerId));
        }

        // Customer response object
        var response = new CustomerLoanResponse(
            customer.Title,
            customer.FirstName,
            customer.LastName,
            customer.DateOfBirth,
            customer.MobileNumber,
            customer.Email,
            customer.Loan.Term,
            customer.Loan.TermInMonths,
            customer.Loan.AmountRequired);

        return Result.Success(response);
    }
}
