using Domain.Entities.Customers;
using Domain.Entities.Loans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.DateOfBirth)
            .IsRequired();

        builder.HasOne(c => c.Loan)
            .WithOne(c => c.Customer)
            .HasForeignKey<Loan>(lo => lo.CustomerId)
            .IsRequired();
    }
}
