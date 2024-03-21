namespace Application.Customers;
public interface ICustomerService
{
    Task<string> Create(CreateLoanCustomerRateRequest request, string redirectUrl, CancellationToken cancellationToken);
    Task<CustomerResponse?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
