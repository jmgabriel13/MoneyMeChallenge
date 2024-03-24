using Application.Interfaces;

namespace Application.Products.Update;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal PerAnnumInterestRate,
    int MinimumDuration,
    int MonthsOfFreeInterest) : ICommand;