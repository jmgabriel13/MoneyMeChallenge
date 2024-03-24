namespace Application.Products.Update;
public sealed record UpdateProductRequest(
    string Name,
    decimal PerAnnumInterestRate,
    int MinimumDuration,
    int MonthsOfFreeInterest,
    int EstablishmentFee);
