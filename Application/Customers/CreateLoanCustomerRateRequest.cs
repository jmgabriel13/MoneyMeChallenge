namespace Application.Customers;
public sealed record CreateLoanCustomerRateRequest(
    string Title,
    string FirstName,
    string LastName,
    string DateOfBirth,
    string MobileNumber,
    string Email,
    string Term,
    string AmountRequired);
