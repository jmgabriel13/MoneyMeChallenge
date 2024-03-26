using Application.Interfaces;

namespace Application.Customers.UpdateCustomerInfo;

public sealed record UpdateCustomerInfoCommand(
    Guid CustomerId,
    string FirstName,
    string LastName,
    string Mobile,
    string Email) : ICommand;