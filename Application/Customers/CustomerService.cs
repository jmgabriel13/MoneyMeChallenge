using Application.Interfaces;
using Application.Products;
using Domain.Entities.Customers;
using Domain.Entities.LoanApplications;
using Domain.Shared;
using Microsoft.VisualBasic;

namespace Application.Customers;
public sealed class CustomerService(
    ICustomerRepository _customerRepository,
    IProductRepository _productRepository,
    IUnitOfWork _unitOfWork) : ICustomerService
{
    public async Task<Result<string>> Create(CreateLoanCustomerRateRequest request, string redirectUrl, CancellationToken cancellationToken)
    {
        bool isValidDate = DateTime.TryParse(request.DateOfBirth, out DateTime parsedDate);
        if (!isValidDate)
        {
            return Result.Failure<string>(new Error("Customer.DateOfBirth", "Date of Birth is not valid"));
        }

        // Check if firstName, lastName and dateOfBirth is existing, then return same redirect URL
        var customer = await _customerRepository.GetByFirstLastNameAndDateOfBirthAsync(
            request.FirstName,
            request.LastName,
            parsedDate,
            cancellationToken);

        if (customer is not null)
        {
            // If customer is existing using firstName, lastName and dateOfbirth, return same redirectUrl
            return Result.Success(customer.RedirectURL);
        }

        // Creating new customer with loan
        var newCustomer = Customer.Create(
            Guid.NewGuid(),
            request.Title,
            request.FirstName,
            request.LastName,
            parsedDate,
            request.MobileNumber,
            request.Email,
            redirectUrl,
            request.Term,
            request.AmountRequired);

        _customerRepository.Add(newCustomer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(newCustomer.RedirectURL);
    }

    public async Task<Result<CustomerLoanResponse>> FindCustomerLoanByIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.FindByIdAsync(customerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<CustomerLoanResponse>(new Error("Customer.NotFound", "Customer not founnd"));
        }

        // Customer response object
        var response = new CustomerLoanResponse(
            customer.Title,
            customer.FirstName,
            customer.LastName,
            customer.DateOfBirth,
            customer.MobileNumber,
            customer.Email,
            customer.Loan.Term,
            customer.Loan.TermInMonths,
            customer.Loan.AmountRequired);

        return Result.Success(response);
    }

    public async Task<Result<CustomerQuoteResponse>> CalculateCustomerQuoteAsync(CustomerQuoteRequest request, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.FindByIdAsync(request.CustomerId, cancellationToken);
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (customer is null || product is null)
        {
            // Return error, cant proceed if customer or product are not found
            return Result.Failure<CustomerQuoteResponse>(new Error("Customer.Calculate.Quote", "Customer or product not found"));
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
            customer.MobileNumber,
            customer.Email,
            customer.Loan.AmountRequired,
            customer.Loan.TermInMonths,
            (decimal)monthlyPayment,
            nameof(RepaymentFrequency.Monthly),
            product.PerAnnumInterestRate,
            (decimal)monthlyInterestRate,
            totalRepayments + 300,
            300,
            totalRepayments - customer.Loan.AmountRequired);

        return Result.Success(quote);
    }
}
