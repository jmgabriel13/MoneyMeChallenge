namespace Application.Customers;
public sealed record CustomerQuoteResponse(
    string FirstName,
    string LastName,
    string MobileNumber,
    string Email,
    int PrincipalAmount,
    int TermInMonths,
    decimal Repayment,
    string RepaymentFrequency,
    decimal PerAnnumInterestRate,
    decimal MonthlyInterestRate,
    decimal TotalRepayments,
    int EstablishmentFee,
    decimal TotalInterest);
