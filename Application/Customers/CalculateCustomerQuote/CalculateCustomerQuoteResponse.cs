namespace Application.Customers.CalculateCustomerQuote;
public sealed record CalculateCustomerQuoteResponse(
    string FirstName,
    string LastName,
    string Mobile,
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
