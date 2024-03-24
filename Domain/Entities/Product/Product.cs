namespace Domain.Entities.Product;
public class Product
{
    private const int _establishmentFee = 300;

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal PerAnnumInterestRate { get; private set; }
    public int MinimumDuration { get; private set; }
    public int MonthsOfFreeInterest { get; private set; }
    public int EstablishmentFee { get; private set; } = _establishmentFee;

    public Product(
        Guid id,
        string name,
        decimal perAnnumInterestRate,
        int minimumDuration,
        int monthsOfFreeInterest,
        int establishmentFee)
    {
        Id = id;
        Name = name;
        PerAnnumInterestRate = perAnnumInterestRate;
        MinimumDuration = minimumDuration;
        MonthsOfFreeInterest = monthsOfFreeInterest;
        EstablishmentFee = establishmentFee;
    }

    private Product()
    {
    }

    public void Update(
        string name,
        decimal perAnnumInterestRate,
        int minimumDuration,
        int monthsOfFreeInterest,
        int establishmentFee)
    {
        Name = name;
        PerAnnumInterestRate = perAnnumInterestRate;
        MinimumDuration = minimumDuration;
        MonthsOfFreeInterest = monthsOfFreeInterest;
        EstablishmentFee = establishmentFee;
    }
}
