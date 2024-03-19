using Domain.Entities.Customers;

namespace Domain.Entities.Loans;
public class Loan
{
    public Guid Id { get; set; }
    public int Term { get; set; }
    public int AmountRequired { get; set; }

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
}
