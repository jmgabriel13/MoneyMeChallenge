using Application.Interfaces;

namespace Application.Customers.CalculateCustomerQuote;
public sealed record CalculateCustomerQuoteRequest(
     Guid CustomerId,
    Guid ProductId,
    int AmountRequired,
    int TermInMonths) : ICommand<CalculateCustomerQuoteResponse>;
