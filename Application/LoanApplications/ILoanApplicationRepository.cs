using Domain.Entities.LoanApplications;

namespace Application.LoanApplications;
public interface ILoanApplicationRepository
{
    void Add(LoanApplicaton loanApplicaton);
    Task<bool> CustomerHasPendingLoanApplication(Guid customerId, CancellationToken cancellationToken = default);
}
