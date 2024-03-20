using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(100);

        builder.Property(p => p.PerAnnumInterestRate)
            .HasColumnType("decimal(18,4)");

        // Seeding data
        builder.HasData(
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product A",
                PerAnnumInterestRate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product B",
                PerAnnumInterestRate = 9.20M
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product C",
                PerAnnumInterestRate = 10.58M
            }
        );
    }
}
