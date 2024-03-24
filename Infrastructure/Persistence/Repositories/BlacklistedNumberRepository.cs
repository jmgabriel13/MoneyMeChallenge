using Application.Blacklists;
using Domain.Entities.Blacklists;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class BlacklistedNumberRepository(
    ApplicationDbContext _context) : IBlacklistedMobileNumberRepository
{
    public void Add(BlacklistedMobileNumber number)
    {
        _context.BlacklistedMobileNumbers.Add(number);
    }

    public async Task<IEnumerable<BlacklistedMobileNumber>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.BlacklistedMobileNumbers.ToListAsync(cancellationToken);
    }

    public async Task<BlacklistedMobileNumber> GetByMobileNumberAsync(
        string number,
        CancellationToken cancellationToken = default)
    {
        return await _context.BlacklistedMobileNumbers
            .FirstOrDefaultAsync(b =>
            b.Value == number,
            cancellationToken);
    }

    public void Update(BlacklistedMobileNumber number)
    {
        _context.Update(number);
    }

    public void Delete(BlacklistedMobileNumber number)
    {
        _context.BlacklistedMobileNumbers.Remove(number);
    }
}

