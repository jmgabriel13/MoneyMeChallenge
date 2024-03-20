using Domain.Entities.Product;

namespace Application.Products;
public interface IProductRepository
{
    void Add(Product product);
    Task<Product> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);
}
