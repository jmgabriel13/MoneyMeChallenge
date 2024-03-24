namespace Domain.Entities.Product;
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal PerAnnumInterestRate { get; set; }
    public int MinimumDuration { get; set; }
    public int MonthsOfFreeInterest { get; set; }

}
