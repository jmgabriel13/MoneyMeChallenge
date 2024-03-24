namespace Domain.Entities.Blacklists;
public class Blacklist
{
    public int Id { get; set; }
    public string Type { get; set; } // "MobileNumber", "EmailDomain", etc.
    public string Value { get; set; } // Blacklisted value
    public string Description { get; set; } // Optional description
}
