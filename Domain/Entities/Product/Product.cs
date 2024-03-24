namespace Domain.Entities.Product;
public class Product
{
    public Product(Guid id, string name, decimal perAnnumInterestRate, int minimumDuration, int monthsOfFreeInterest)
    {
        Id = id;
        Name = name;
        PerAnnumInterestRate = perAnnumInterestRate;
        MinimumDuration = minimumDuration;
        MonthsOfFreeInterest = monthsOfFreeInterest;
    }

    private Product()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal PerAnnumInterestRate { get; private set; }
    public int MinimumDuration { get; private set; }
    public int MonthsOfFreeInterest { get; private set; }

    public void Update(string name, decimal perAnnumInterestRate, int minimumDuration, int monthsOfFreeInterest)
    {
        Name = name;
        PerAnnumInterestRate = perAnnumInterestRate;
        MinimumDuration = minimumDuration;
        MonthsOfFreeInterest = monthsOfFreeInterest;
    }
}
