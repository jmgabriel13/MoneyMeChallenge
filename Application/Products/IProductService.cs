using Domain.Entities.Product;

namespace Application.Products;
public interface IProductService
{
    Task<IReadOnlyCollection<Product>> GetAllProducts(CancellationToken cancellationToken = default);
}
