using Application.LoanApplications;
using Domain.Entities.LoanApplications;

namespace Infrastructure.Persistence.Repositories;
public sealed class LoanApplicationRepository : ILoanApplicationRepository
{
    private readonly ApplicationDbContext _context;

    public LoanApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(LoanApplicaton loanApplicaton)
    {
        _context.Add(loanApplicaton);
    }
}
