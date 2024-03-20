namespace Application.Customers;
public interface ICustomerLoanRateService
{
    Task<string> Create(CreateLoanCustomerRateRequest request, string redirectUrl, CancellationToken cancellationToken);
}
