using Application.Blacklists;
using Application.Customers;
using Application.Interfaces;
using Domain.Entities.Customers;
using Domain.Entities.LoanApplications;
using Domain.Errors;
using Domain.Shared;

namespace Application.LoanApplications.CreateLoanApplication;
internal sealed class CreateLoanApplicationHandler(
    ILoanApplicationRepository _loanApplicationRepository,
    ICustomerRepository _customerRepository,
    IBlacklistedEmailDomainRepository _emailDomainRepository,
    IBlacklistedMobileNumberRepository _mobileNumberRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<CreateLoanApplicationRequest>
{
    public async Task<Result> Handle(CreateLoanApplicationRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.FindByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            // Return error customer not found
            return Result.Failure(DomainErrors.Customer.CustomerNotFound(request.CustomerId));
        }

        // Check if customer has a pending loan application
        if (await _loanApplicationRepository.CustomerHasPendingLoanApplication(
            customer.Id,
            cancellationToken))
        {
            // Return error that customer has a pending loan application
            return Result.Failure(DomainErrors.Customer.HasPendingLoanApplication);
        }

        // Perform customer eligibility
        if (!await IsEligibleAsync(customer))
        {
            // Not eligible error message
            return Result.Failure(DomainErrors.Customer.NotEligible);
        }

        // Creating instance of loanApplication
        var loanApplication = LoanApplicaton.Create(
            Guid.NewGuid(),
            request.CustomerId,
            request.RepaymentFrequency,
            request.Repayment,
            request.TotalRepayments,
            request.InterestRate,
            request.Interest,
            LoanStatus.Pending);

        _loanApplicationRepository.Add(loanApplication);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<bool> IsEligibleAsync(Customer customer)
    {
        // Check if the customer is at least 18 years old
        if (DateTime.Today.AddYears(-18) < customer.DateOfBirth)
        {
            return false;
        }

        // Check if the mobile number is blacklisted
        if (await _mobileNumberRepository.GetByMobileNumberAsync(customer.Mobile) != null)
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
