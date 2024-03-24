using Application.Interfaces;

namespace Application.Products.Update;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal PerAnnumInterestRate,
    int MinimumDuration,
    int MonthsOfFreeInterest,
    int EstablishmentFee) : ICommand;