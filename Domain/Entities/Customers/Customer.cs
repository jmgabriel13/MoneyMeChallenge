namespace Domain.Entities.Customers;
public class Customer
{
    public Guid CustomerId { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
    public string RedirectURL { get; set; }
}
