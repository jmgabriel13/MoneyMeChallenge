namespace Application.Customers.CreateCustomerLoanRate;
public sealed record CreateCustomerLoanRateRequest(
    string AmountRequired,
    string Term,
    string Title,
    string FirstName,
    string LastName,
    string DateOfBirth,
    string Mobile,
    string Email);
