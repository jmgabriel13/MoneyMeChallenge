using Application.Interfaces;
using Application.Products;
using Domain.Entities.LoanApplications;
using Domain.Errors;
using Domain.Shared;
using Microsoft.VisualBasic;

namespace Application.Customers.CalculateCustomerQuote;
internal sealed class CalculateCustomerQuoteHandler(
    ICustomerRepository _customerRepository,
    IProductRepository _productRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<CalculateCustomerQuoteRequest, CalculateCustomerQuoteResponse>
{
    public async Task<Result<CalculateCustomerQuoteResponse>> Handle(CalculateCustomerQuoteRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindByIdAsync(request.CustomerId, cancellationToken);
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (customer is null || product is null)
        {
            // Return error, cant proceed if customer or product are not found
            return Result.Failure<CalculateCustomerQuoteResponse>(DomainErrors.Customer.CustomerOrProductNotFound);
        }

        // Checking if theres a changes in amount and term,
        // update if theres a changes
        if (customer.Loan.AmountRequired != request.AmountRequired || customer.Loan.TermInMonths != request.TermInMonths)
        {
            // Update Customer loan
            customer.Loan.Update(request.TermInMonths, request.AmountRequired);
            _customerRepository.Update(customer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        // calculate the monthly repayment amount using PMT function
        // first convert PerAnnumInterestRate to a decimal value
        // then divide it by 12months(1yr) to convert it as percentage.
        double monthlyInterestRate = (double)product.PerAnnumInterestRate / 100 / (int)RepaymentFrequency.Monthly;
        // calculate using PMT Function
        double monthlyPayment = -Financial.Pmt(
            monthlyInterestRate,
            customer.Loan.TermInMonths,
            customer.Loan.AmountRequired);
        // calculate total repayments based on term in months
        decimal totalRepayments = (decimal)monthlyPayment * customer.Loan.TermInMonths;

        // Check if theres a month(s) of free interest in product.
        if (product.MonthsOfFreeInterest > 0)
        {
            // Then minus the monthlyPayment multiply by MonthsOfFreeInterest to totalRepaymens
            // To get only the sum of monthlyPayment that has a interest.
            totalRepayments -= (decimal)monthlyPayment * product.MonthsOfFreeInterest;
        }

        decimal monthlyPaymentFinal = (decimal)monthlyPayment;
        decimal totalRepaymentsFinal = totalRepayments + product.EstablishmentFee;
        decimal totalInterestFinal = totalRepayments - customer.Loan.AmountRequired;

        // Monthly Iterest Rate back to decimal
        decimal monthlyInterestRateDecimalFinal = Math.Round((decimal)monthlyInterestRate * 100, 2);

        var quote = new CalculateCustomerQuoteResponse(
            customer.FirstName,
            customer.LastName,
            customer.Mobile,
            customer.Email,
            customer.Loan.AmountRequired,
            customer.Loan.TermInMonths,
            monthlyPaymentFinal,
            nameof(RepaymentFrequency.Monthly),
            product.PerAnnumInterestRate,
            monthlyInterestRateDecimalFinal,
            totalRepaymentsFinal,
            product.EstablishmentFee,
            totalInterestFinal);

        return Result.Success(quote);
    }
}
