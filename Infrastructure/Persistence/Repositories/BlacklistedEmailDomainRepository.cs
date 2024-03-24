using Application.Blacklists;
using Domain.Entities.Blacklists;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class BlacklistedEmailDomainRepository(
    ApplicationDbContext _context) : IBlacklistedEmailDomainRepository
{
    public void Add(BlacklistedEmailDomain emailDomain)
    {
        _context.BlacklistedEmailDomains.Add(emailDomain);
    }

    public async Task<IEnumerable<BlacklistedEmailDomain>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.BlacklistedEmailDomains.ToListAsync(cancellationToken);
    }

    public async Task<BlacklistedEmailDomain> GetByEmailDomainAsync(
        string emailDomain,
        CancellationToken cancellationToken = default)
    {
        return await _context.BlacklistedEmailDomains
            .FirstOrDefaultAsync(b =>
            b.Value.ToLower() == emailDomain,
            cancellationToken);
    }

    public void Update(BlacklistedEmailDomain emailDomain)
    {
        _context.BlacklistedEmailDomains.Update(emailDomain);
    }

    public void Delete(BlacklistedEmailDomain emailDomain)
    {
        _context.BlacklistedEmailDomains.Remove(emailDomain);
    }
}

