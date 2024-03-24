using Domain.Entities.Blacklists;

namespace Application.Blacklists;

public interface IBlacklistedEmailDomainRepository
{
    void Add(BlacklistedEmailDomain emailDomain);
    Task<IEnumerable<BlacklistedEmailDomain>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BlacklistedEmailDomain> GetByEmailDomainAsync(string emailDomain, CancellationToken cancellationToken = default);
    void Update(BlacklistedEmailDomain emailDomain);
    void Delete(BlacklistedEmailDomain emailDomain);
}
