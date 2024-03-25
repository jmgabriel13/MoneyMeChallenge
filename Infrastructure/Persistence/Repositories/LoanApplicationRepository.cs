using Application.LoanApplications;
using Domain.Entities.LoanApplications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class LoanApplicationRepository(
    ApplicationDbContext _context) : ILoanApplicationRepository
{
    public void Add(LoanApplicaton loanApplicaton)
    {
        _context.Add(loanApplicaton);
    }

    public async Task<bool> CustomerHasPendingLoanApplication(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _context.LoanApplicatons
            .AnyAsync(l =>
            l.CustomerId == customerId &&
            l.Status == LoanStatus.Pending,
            cancellationToken);
    }
}
