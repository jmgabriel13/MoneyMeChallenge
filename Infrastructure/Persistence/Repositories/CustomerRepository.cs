using Application.Customers;
using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class CustomerRepository : ICustomerRepository
{

    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

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
            .FirstOrDefaultAsync(
            c => c.FirstName == firstName &&
            c.LastName == lastName &&
            c.DateOfBirth == dateOfBirth,
            cancellationToken: cancellationToken);
    }

    public Task<bool> IsCustomerExist(string firstName, string lastName, DateTime dateOfBirth, CancellationToken cancellationToken = default)
    {
        return _context.Customers
            .AnyAsync(
            c => c.FirstName == firstName &&
            c.LastName == lastName &&
            c.DateOfBirth == dateOfBirth,
            cancellationToken: cancellationToken);
    }
}
