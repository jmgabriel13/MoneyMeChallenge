using Domain.Entities.Blacklists;

namespace Application.Blacklists;

public interface IBlacklistedMobileNumberRepository
{
    void Add(BlacklistedMobileNumber number);
    Task<IEnumerable<BlacklistedMobileNumber>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BlacklistedMobileNumber> GetByMobileNumberAsync(string number, CancellationToken cancellationToken = default);
    void Update(BlacklistedMobileNumber number);
    void Delete(BlacklistedMobileNumber number);
}
