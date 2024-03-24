using Domain.Entities.Blacklists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public sealed class BlacklistedMobileNumberConfiguration : IEntityTypeConfiguration<BlacklistedMobileNumber>
{
    public void Configure(EntityTypeBuilder<BlacklistedMobileNumber> builder)
    {
        builder.HasKey(b => b.Id);

        // Seeding data
        builder.HasData(
            new BlacklistedMobileNumber
            {
                Id = Guid.NewGuid(),
                Value = "09123456789"
            },
            new BlacklistedMobileNumber
            {
                Id = Guid.NewGuid(),
                Value = "09987654321"
            },
            new BlacklistedMobileNumber
            {
                Id = Guid.NewGuid(),
                Value = "12312312312"
            }
        );
    }
}