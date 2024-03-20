namespace Application.LoanApplications;
public interface ILoanApplicationService
{
    Task Create(CreateLoanApplicationRequest request, CancellationToken cancellationToken = default);
}
