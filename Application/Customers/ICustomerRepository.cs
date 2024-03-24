using Domain.Entities.Customers;

namespace Application.Customers;
public interface ICustomerRepository
{
    void Add(Customer customer);
    Task<Customer?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Customer?> GetByFirstLastNameAndDateOfBirthAsync(string firstName, string lastName, DateTime dateOfBirth, CancellationToken cancellationToken = default);
}
