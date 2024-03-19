using Domain.Entities.LoanApplications;

namespace Application.LoanApplications;
public interface ILoanApplicationRepository
{
    void Add(LoanApplicaton loanApplicaton);
}
