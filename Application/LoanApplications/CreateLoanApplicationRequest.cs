namespace Application.LoanApplications;
public sealed record CreateLoanApplicationRequest(
    Guid CustomerId,
    Guid ProductId,
    decimal Repayments);