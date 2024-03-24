namespace Application.Products.Update;
public record UpdateProductRequest(
    string Name,
    decimal PerAnnumInterestRate,
    int MinimumDuration,
    int MonthsOfFreeInterest,
    int EstablishmentFee);
