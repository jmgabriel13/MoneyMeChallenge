using Application.Blacklists;
using Application.Customers;
using Application.Interfaces;
using Domain.Entities.Customers;
using Domain.Entities.LoanApplications;

namespace Application.LoanApplications;
public class LoanApplicationService : ILoanApplicationService
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IBlacklistedEmailDomainRepository _emailDomainRepository;
    private readonly IBlacklistedMobileNumberRepository _mobileNumberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoanApplicationService(
        ILoanApplicationRepository loanApplicationRepository,
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository,
        IBlacklistedEmailDomainRepository emailDomainRepository,
        IBlacklistedMobileNumberRepository mobileNumberRepository)
    {
        _loanApplicationRepository = loanApplicationRepository;
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _emailDomainRepository = emailDomainRepository;
        _mobileNumberRepository = mobileNumberRepository;
    }

    public async Task Create(CreateLoanApplicationRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            // Return error customer not found
            return;
        }

        // Check if customer has a pending loan application
        if (await _loanApplicationRepository.CustomerHasPendingLoanApplication(
            customer.Id,
            cancellationToken))
        {
            // Return error that customer has a pending loan application
            return;
        }

        // Perform customer eligibility
        if (!await IsEligibleAsync(customer))
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

    public async Task<bool> IsEligibleAsync(Customer customer)
    {
        // Check if the customer is at least 18 years old
        if (DateTime.Today.AddYears(-18) < customer.DateOfBirth)
        {
            return false;
        }

        // Check if the mobile number is blacklisted
        if (await _mobileNumberRepository.GetByMobileNumberAsync(customer.MobileNumber) != null)
        {
            return false;
        }

        // Check if the email domain is blacklisted
        string emailDomain = customer.Email.Split('@')[1];
        if (await _emailDomainRepository.GetByEmailDomainAsync(emailDomain) != null)
        {
            return false;
        }

        return true;
    }
}
