namespace Application.Customers.GetCustomerLoanById;
public sealed record CustomerLoanResponse(
    string Title,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string MobileNumber,
    string Email,
    decimal Term,
    int TermInMonths,
    int AmountRequired);
