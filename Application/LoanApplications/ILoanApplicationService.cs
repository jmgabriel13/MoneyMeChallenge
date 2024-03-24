using Domain.Shared;

namespace Application.LoanApplications;
public interface ILoanApplicationService
{
    Task<Result> Create(CreateLoanApplicationRequest request, CancellationToken cancellationToken = default);
}
