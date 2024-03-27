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
        // then divide it by 12months(1yr) to convert it to percentage.
        double monthlyInterestRate = (double)product.PerAnnumInterestRate / 100 / (int)RepaymentFrequency.Monthly;

        // consider other fees
        //int principalAmount = customer.Loan.AmountRequired + product.EstablishmentFee;
        int principalAmount = customer.Loan.AmountRequired;

        decimal otherFees = decimal.Divide(product.EstablishmentFee, customer.Loan.TermInMonths);

        // calculate using PMT Function to get monthly payment WITH interest based on monthly interest rate
        double monthlyPaymentWithInterestFinal = -Financial.Pmt(
            monthlyInterestRate,
            customer.Loan.TermInMonths,
            principalAmount);

        // add other fees
        monthlyPaymentWithInterestFinal += (double)otherFees;

        // calculate total repayments with interest based on term in months and other fees included
        decimal totalRepaymentsWithInterestFinal = (decimal)monthlyPaymentWithInterestFinal * customer.Loan.TermInMonths;
        // Calculate first the totalInterest by subtracting principal amount(AmountRequired) to total repayments with interest
        decimal totalInterestFinal = totalRepaymentsWithInterestFinal - principalAmount;

        // Check if theres a month(s) of free interest in product.
        double monthlyPaymentWithoutInterestFinal = 0;
        if (product.MonthsOfFreeInterest > 0)
        {
            // calculate using PMT Function to get monthly payment WITHOUT interest
            monthlyPaymentWithoutInterestFinal = -Financial.Pmt(
                0,
                customer.Loan.TermInMonths,
                principalAmount);

            // get the monthly interest by dividing total interest from loan term in months
            decimal monthlyInterest = totalInterestFinal / customer.Loan.TermInMonths;
            // multiply the monthlyInterest to the product months of free interest
            decimal totalMonthsOfFreeInterest = monthlyInterest * product.MonthsOfFreeInterest;

            // deduct the totalMonthsOfFreeInterest to the final values
            totalInterestFinal -= totalMonthsOfFreeInterest;
            // recompute total repayments
            totalRepaymentsWithInterestFinal = (decimal)monthlyPaymentWithoutInterestFinal * customer.Loan.TermInMonths + totalInterestFinal;
        }

        // Monthly Iterest Rate back to decimal
        decimal monthlyInterestRateDecimalFinal = Math.Round((decimal)monthlyInterestRate * 100, 2);

        var quote = new CalculateCustomerQuoteResponse(
            customer.FirstName,
            customer.LastName,
            customer.Mobile,
            customer.Email,
            customer.Loan.AmountRequired,
            customer.Loan.TermInMonths,
            (decimal)monthlyPaymentWithInterestFinal,
            (decimal)monthlyPaymentWithoutInterestFinal,
            nameof(RepaymentFrequency.Monthly),
            product.PerAnnumInterestRate,
            monthlyInterestRateDecimalFinal,
            totalRepaymentsWithInterestFinal,
            product.EstablishmentFee,
            totalInterestFinal,
            product.MonthsOfFreeInterest);

        return Result.Success(quote);
    }
}
