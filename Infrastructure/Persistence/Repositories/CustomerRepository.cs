using Application.Customers;
using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class CustomerRepository(
    ApplicationDbContext _context) : ICustomerRepository
{
    public void Add(Customer customer)
    {
        _context.Add(customer);
    }

    public Task<Customer?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Customers
            .Include(c => c.Loan)
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public Task<Customer?> GetByFirstLastNameAndDateOfBirthAsync(string firstName, string lastName, DateTime dateOfBirth, CancellationToken cancellationToken = default)
    {
        return _context.Customers
            .FirstOrDefaultAsync(c =>
            c.FirstName.ToLower() == firstName.ToLower() &&
            c.LastName.ToLower() == lastName.ToLower() &&
            c.DateOfBirth == dateOfBirth,
            cancellationToken: cancellationToken);
    }

    public void Update(Customer customer)
    {
        _context.Update(customer);
    }
}
