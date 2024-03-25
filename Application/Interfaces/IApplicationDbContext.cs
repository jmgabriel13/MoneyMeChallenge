using Domain.Entities.Blacklists;
using Domain.Entities.Customers;
using Domain.Entities.LoanApplications;
using Domain.Entities.Loans;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;
public interface IApplicationDbContext
{
    DbSet<LoanApplicaton> LoanApplicatons { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Loan> Loans { get; set; }
    DbSet<BlacklistedEmailDomain> BlacklistedEmailDomains { get; set; }
    DbSet<BlacklistedMobileNumber> BlacklistedMobileNumbers { get; set; }
}