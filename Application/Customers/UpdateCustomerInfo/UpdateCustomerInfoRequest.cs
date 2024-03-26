namespace Application.Customers.UpdateCustomerInfo;
public sealed record UpdateCustomerInfoRequest(
    string FirstName,
    string LastName,
    string Mobile,
    string Email);
