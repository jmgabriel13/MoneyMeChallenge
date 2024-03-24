using Application.Interfaces;

namespace Application.Products.Create;
public sealed record CreateProductCommand(
    string Name,
    decimal PerAnnumInterestRate,
    int MinimumDuration,
    int MonthsOfFreeInterest,
    int EstablishmentFee) : ICommand;
