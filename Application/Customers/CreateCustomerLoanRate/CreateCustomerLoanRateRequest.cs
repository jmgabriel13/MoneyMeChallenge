namespace Application.Customers.CreateCustomerLoanRate;
public sealed record CreateCustomerLoanRateRequest(
    string Title,
    string FirstName,
    string LastName,
    string DateOfBirth,
    string MobileNumber,
    string Email,
    string Term,
    string AmountRequired);
