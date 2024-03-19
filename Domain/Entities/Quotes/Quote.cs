namespace Domain.Entities.Quotes;
public class Quote
{
    public Guid QouteId { get; set; }
    public string Term { get; set; }
    public int AmountRequired { get; set; }
}
