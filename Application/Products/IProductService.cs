using Domain.Entities.Product;
using Domain.Shared;

namespace Application.Products;
public interface IProductService
{
    Task<Result<IReadOnlyCollection<Product>>> GetAllProducts(CancellationToken cancellationToken = default);
}
