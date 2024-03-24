namespace Application.Customers;
public record CustomerQuoteRequest(
    Guid CustomerId,
    Guid ProductId);
