using Application.Interfaces;
using Domain.Entities.Blacklists;
using Domain.Entities.Customers;
using Domain.Entities.LoanApplications;
using Domain.Entities.Loans;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public sealed class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<LoanApplicaton> LoanApplicatons { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<BlacklistedEmailDomain> BlacklistedEmailDomains { get; set; }
    public DbSet<BlacklistedMobileNumber> BlacklistedMobileNumbers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}