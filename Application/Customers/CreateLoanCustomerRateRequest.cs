namespace Application.Customers;
public sealed record CreateLoanCustomerRateRequest(
    string Title,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string MobileNumber,
    string Email,
    int Term,
    int AmountRequired);
