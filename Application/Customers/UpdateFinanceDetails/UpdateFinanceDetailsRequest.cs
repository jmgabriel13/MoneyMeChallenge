namespace Application.Customers.UpdateFinanceDetails;
public sealed record UpdateFinanceDetailsRequest(
    int AmountRequired,
    int TermInMonths);
