using Domain.Shared;

namespace Application.Customers;
public interface ICustomerService
{
    Task<Result<string>> Create(CreateLoanCustomerRateRequest request, string redirectUrl, CancellationToken cancellationToken);
    Task<Result<CustomerLoanResponse>> FindCustomerLoanByIdAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<Result<CustomerQuoteResponse>> CalculateCustomerQuoteAsync(CustomerQuoteRequest request, CancellationToken cancellationToken = default);
}
