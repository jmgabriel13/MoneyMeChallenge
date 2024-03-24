using Application.Interfaces;

namespace Application.Customers.CalculateCustomerQuote;
public sealed record CalculateCustomerQuoteRequest(
     Guid CustomerId,
    Guid ProductId) : ICommand<CustomerQuoteResponse>;
