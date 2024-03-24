using Application.Customers;
using Application.Interfaces;
using Application.Products;
using Domain.Entities.Customers;
using Domain.Entities.LoanApplications;

namespace Application.LoanApplications;
public class LoanApplicationService : ILoanApplicationService
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoanApplicationService(
        ILoanApplicationRepository loanApplicationRepository,
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        ICustomerRepository customerRepository)
    {
        _loanApplicationRepository = loanApplicationRepository;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
    }

    public async Task Create(CreateLoanApplicationRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return;
        }

        // Perform applicant checking
        if (!IsEligible(customer))
        {
            // Not eligible error message
            return;
        }

        // Creating instance of loanApplication
        var loanApplication = new LoanApplicaton
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            RepaymentFrequency = request.RepaymentFrequency,
            Repayment = request.Repayment,
            TotalRepayments = request.TotalRepayments,
            InterestRate = request.InterestRate,
            Interest = request.Interest,
            Status = LoanStatus.Pending
        };

        _loanApplicationRepository.Add(loanApplication);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return;
    }

    public static bool IsEligible(Customer customer)
    {
        // Check if the customer is at least 18 years old
        if (DateTime.Today.AddYears(-18) < customer.DateOfBirth)
        {
            return false;
        }

        // Check if the mobile number is blacklisted
        if (BlacklistedMobileNumbers.Contains(customer.MobileNumber))
        {
            return false;
        }

        // Check if the email domain is blacklisted
        string emailDomain = customer.Email.Split('@')[1];
        if (BlacklistedDomains.Contains(emailDomain))
        {
            return false;
        }

        return true;
    }

    public static readonly List<string> BlacklistedMobileNumbers =
    [
        // Add blacklisted mobile numbers here
        "123456789",
        "987654321"
    ];

    public static readonly List<string> BlacklistedDomains =
    [
        // Add blacklisted domains here
        "flower.com.au",
        "example.com",
        "test.com"
    ];
}
