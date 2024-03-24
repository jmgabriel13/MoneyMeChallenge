using Domain.Entities.Blacklists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public sealed class BlacklistedEmailDomainConfiguration : IEntityTypeConfiguration<BlacklistedEmailDomain>
{
    public void Configure(EntityTypeBuilder<BlacklistedEmailDomain> builder)
    {
        builder.HasKey(b => b.Id);

        // Seeding data
        builder.HasData(
            new BlacklistedEmailDomain
            {
                Id = Guid.NewGuid(),
                Value = "test.com"
            },
            new BlacklistedEmailDomain
            {
                Id = Guid.NewGuid(),
                Value = "hotmail.com"
            },
            new BlacklistedEmailDomain
            {
                Id = Guid.NewGuid(),
                Value = "blackmail.com"
            }
        );
    }
}
