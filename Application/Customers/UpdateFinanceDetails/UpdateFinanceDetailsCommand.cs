using Application.Interfaces;

namespace Application.Customers.UpdateFinanceDetails;

public sealed record UpdateFinanceDetailsCommand(
    Guid CustomerId,
    int AmountRequired,
    int TermInMonths) : ICommand;
