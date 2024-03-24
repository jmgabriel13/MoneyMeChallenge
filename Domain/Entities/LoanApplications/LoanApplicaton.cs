namespace Domain.Entities.LoanApplications;
public class LoanApplicaton
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public string RepaymentFrequency { get; private set; }
    public decimal Repayment { get; private set; }
    public decimal TotalRepayments { get; private set; }
    public decimal Interest { get; private set; }
    public decimal InterestRate { get; private set; }
    public LoanStatus Status { get; private set; }

    private LoanApplicaton(
        Guid id,
        Guid customerId,
        string repaymentFrequency,
        decimal repayment,
        decimal totalRepayments,
        decimal interest,
        decimal interestRate,
        LoanStatus status)
    {
        Id = id;
        CustomerId = customerId;
        RepaymentFrequency = repaymentFrequency;
        Repayment = repayment;
        TotalRepayments = totalRepayments;
        Interest = interest;
        InterestRate = interestRate;
        Status = status;
    }

    private LoanApplicaton()
    {
    }

    public static LoanApplicaton Create(
        Guid id,
        Guid customerId,
        string repaymentFrequency,
        decimal repayment,
        decimal totalRepayments,
        decimal interest,
        decimal interestRate,
        LoanStatus status)
    {
        var loanApplication = new LoanApplicaton(
            id,
            customerId,
            repaymentFrequency,
            repayment,
            totalRepayments,
            interest,
            interestRate,
            status);

        return loanApplication;
    }
}
