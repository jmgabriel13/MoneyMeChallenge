namespace Domain.Entities.CalculatedQoutes;
public class CalculatedQuote
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid QouteId { get; set; }
}
