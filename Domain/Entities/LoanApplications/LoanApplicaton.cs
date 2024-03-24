namespace Domain.Entities.LoanApplications;
public class LoanApplicaton
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string RepaymentFrequency { get; set; }
    public decimal Repayment { get; set; }
    public decimal TotalRepayments { get; set; }
    public decimal Interest { get; set; }
    public decimal InterestRate { get; set; }
    public LoanStatus Status { get; set; }
}
