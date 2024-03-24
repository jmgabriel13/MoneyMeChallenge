using Application.Interfaces;
using Application.Products;
using Domain.Entities.Customers;
using Domain.Entities.LoanApplications;
using Domain.Entities.Loans;
using Microsoft.VisualBasic;

namespace Application.Customers;
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        IProductRepository productRepository)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<string> Create(CreateLoanCustomerRateRequest request, string redirectUrl, CancellationToken cancellationToken)
    {
        bool isValidDate = DateTime.TryParse(request.DateOfBirth, out DateTime parsedDate);
        if (!isValidDate)
        {
            // Return 
            return "Date of Birth is not valid.";
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
            return customer.RedirectURL;
        }

        // Creating instance of loan
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            Term = decimal.Parse(request.Term),
            TermInMonths = YearsToMonths(decimal.Parse(request.Term)),
            AmountRequired = Int32.Parse(request.AmountRequired)
        };

        // Creating new customer
        var newCustomerId = Guid.NewGuid();
        var newCustomer = new Customer
        {
            Id = newCustomerId,
            Title = request.Title,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = parsedDate,
            MobileNumber = request.MobileNumber,
            Email = request.Email,
            RedirectURL = $"{redirectUrl}/quote-calculator?customerId={newCustomerId}",
            Loan = loan
        };

        _customerRepository.Add(newCustomer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newCustomer.RedirectURL;
    }

    public async Task<CustomerLoanResponse?> FindCustomerLoanByIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.FindByIdAsync(customerId, cancellationToken);

        if (customer is null)
        {
            throw new InvalidOperationException("Customer not found");
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

        return response;
    }

    public async Task<CustomerQuoteResponse?> CalculateCustomerQuoteAsync(CustomerQuoteRequest request, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.FindByIdAsync(request.CustomerId, cancellationToken);
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (customer is null || product is null)
        {
            // Return error, cant proceed if customer or product are not found
            throw new InvalidOperationException("Customer or product not found");
        }

        // calculate the monthly repayment amount using PMT function
        double monthlyRate = (double)product.PerAnnumInterestRate / 100 / (int)RepaymentFrequency.Monthly;
        double monthlyPayment = -Financial.Pmt(
            monthlyRate,
            (double)(customer.Loan.Term * (int)RepaymentFrequency.Monthly),
            customer.Loan.AmountRequired);
        decimal totalRepayments = (decimal)monthlyPayment * (customer.Loan.Term * (int)RepaymentFrequency.Monthly);

        var quote = new CustomerQuoteResponse(
            customer.FirstName,
            customer.LastName,
            customer.MobileNumber,
            customer.Email,
            customer.Loan.AmountRequired,
            customer.Loan.TermInMonths,
            (decimal)monthlyPayment,
            nameof(RepaymentFrequency.Monthly),
            totalRepayments,
            300,
            totalRepayments - customer.Loan.AmountRequired);

        return quote;
    }

    public static int YearsToMonths(decimal years)
    {
        if (years < 0)
        {
            throw new ArgumentException("Invalid input: Please provide a non-negative number of years.", nameof(years));
        }

        return (int)Math.Round(years * 12);
    }
}
