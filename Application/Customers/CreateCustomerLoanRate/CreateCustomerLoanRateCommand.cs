using Application.Interfaces;

namespace Application.Customers.CreateCustomerLoanRate;
public sealed record CreateCustomerLoanRateCommand(
    string Title,
    string FirstName,
    string LastName,
    string DateOfBirth,
    string MobileNumber,
    string Email,
    string Term,
    string AmountRequired,
    string redirectUrl) : ICommand<string>;
