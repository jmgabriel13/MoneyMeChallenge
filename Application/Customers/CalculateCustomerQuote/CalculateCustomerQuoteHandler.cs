using Application.Interfaces;
using Application.Products;
using Domain.Entities.LoanApplications;
using Domain.Errors;
using Domain.Shared;
using Microsoft.VisualBasic;

namespace Application.Customers.CalculateCustomerQuote;
internal sealed class CalculateCustomerQuoteHandler(
    ICustomerRepository _customerRepository,
    IProductRepository _productRepository) : ICommandHandler<CalculateCustomerQuoteRequest, CustomerQuoteResponse>
{
    public async Task<Result<CustomerQuoteResponse>> Handle(CalculateCustomerQuoteRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindByIdAsync(request.CustomerId, cancellationToken);
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (customer is null || product is null)
        {
            // Return error, cant proceed if customer or product are not found
            return Result.Failure<CustomerQuoteResponse>(DomainErrors.Customer.CustomerOrProductNotFound);
        }

        // calculate the monthly repayment amount using PMT function
        // first convert PerAnnumInterestRate to a decimal value then divide it by 12months(1yr)
        double monthlyInterestRate = (double)product.PerAnnumInterestRate / 100 / (int)RepaymentFrequency.Monthly;
        double monthlyPayment = -Financial.Pmt(
            monthlyInterestRate,
            customer.Loan.TermInMonths,
            customer.Loan.AmountRequired);
        decimal totalRepayments = (decimal)monthlyPayment * customer.Loan.TermInMonths;

        // Check if theres a month(s) of free interest in product.
        if (product.MonthsOfFreeInterest > 0)
        {
            // Then minus the monthlyPayment multiply by MonthsOfFreeInterest to totalRepaymens
            // To get only the sum of monthlyPayment that has a interest.
            totalRepayments -= (decimal)monthlyPayment * product.MonthsOfFreeInterest;
        }

        var quote = new CustomerQuoteResponse(
            customer.FirstName,
            customer.LastName,
            customer.Mobile,
            customer.Email,
            customer.Loan.AmountRequired,
            customer.Loan.TermInMonths,
            (decimal)monthlyPayment,
            nameof(RepaymentFrequency.Monthly),
            product.PerAnnumInterestRate,
            (decimal)monthlyInterestRate,
            totalRepayments + product.EstablishmentFee,
            product.EstablishmentFee,
            totalRepayments - customer.Loan.AmountRequired);

        return Result.Success(quote);
    }
}
