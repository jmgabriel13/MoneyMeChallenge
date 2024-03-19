namespace Domain.Entities.LoanApplications;
public class LoanApplicaton
{
    private const int _establishmentFee = 300;

    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalRepayments { get; set; }
    public decimal MonthlyRepayment { get; set; }
    public static int EstablishmentFee => _establishmentFee;
    public decimal Interest { get; set; }
    public decimal InterestRate { get; set; }
    public bool IsApproved { get; set; }
}
