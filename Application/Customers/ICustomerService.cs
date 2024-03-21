namespace Application.Customers;
public interface ICustomerService
{
    Task<string> Create(CreateLoanCustomerRateRequest request, string redirectUrl, CancellationToken cancellationToken);
    Task<CustomerLoanResponse?> FindCustomerLoanByIdAsync(Guid customerId, CancellationToken cancellationToken = default);
}
