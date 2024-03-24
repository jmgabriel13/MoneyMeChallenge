﻿namespace Application.LoanApplications;
public sealed record CreateLoanApplicationRequest(
    Guid CustomerId,
    string RepaymentFrequency,
    decimal Repayment,
    decimal TotalRepayments,
    decimal InterestRate,
    decimal Interest);