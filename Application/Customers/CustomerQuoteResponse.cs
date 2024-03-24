namespace Application.Customers;
public record CustomerQuoteResponse(
    string FirstName,
    string LastName,
    string MobileNumber,
    string Email,
    int PrincipalAmount,
    int TermInMonths,
    decimal Repayment,
    string PaymentFrequency,
    decimal TotalRepayments,
    int EstablishmentFee,
    decimal TotalInterest);
