using Domain.Entities.Customers;
using Domain.Entities.LoanApplications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
internal sealed class LoanApplicationConfiguration : IEntityTypeConfiguration<LoanApplicaton>
{
    public void Configure(EntityTypeBuilder<LoanApplicaton> builder)
    {
        builder.HasKey(la => la.Id);

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(la => la.CustomerId)
            .IsRequired();

        builder.Property(la => la.TotalRepayments)
            .HasColumnType("decimal(18,4)");

        builder.Property(la => la.MonthlyRepayment)
            .HasColumnType("decimal(18,4)");

        builder.Property(la => la.Interest)
            .HasColumnType("decimal(18,4)");

        builder.Property(la => la.InterestRate)
            .HasColumnType("decimal(18,4)");
    }
}
