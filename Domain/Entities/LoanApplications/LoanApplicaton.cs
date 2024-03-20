namespace Domain.Entities.LoanApplications;
public class LoanApplicaton
{
    private const int _establishmentFee = 300;

    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }

    // To be refactor, it needs to be a entity to be more flexible and extensible
    public string RepaymentFrequency { get; set; }
    public decimal Repayment { get; set; }
    public decimal TotalRepayments { get; set; }
    public int EstablishmentFee => _establishmentFee;
    public decimal Interest { get; set; }
    public decimal InterestRate { get; set; }
    public LoanStatus Status { get; set; }
}
