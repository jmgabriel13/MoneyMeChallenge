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
            new Product(
                Guid.NewGuid(),
                "Product A",
                0,
                1,
                0),
            new Product(
                Guid.NewGuid(),
                "Product B",
                9.20M,
                6,
                2),
            new Product(
                Guid.NewGuid(),
                "Product C",
                10.58M,
                1,
                0)
        );
    }
}
