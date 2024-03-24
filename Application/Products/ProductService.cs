using Domain.Entities.Product;
using Domain.Shared;

namespace Application.Products;
public sealed class ProductService(
    IProductRepository _productRepository) : IProductService
{
    public async Task<Result<IReadOnlyCollection<Product>>> GetAllProducts(CancellationToken cancellationToken = default)
    {
        return await _productRepository.GetAllProductsAsync(cancellationToken);
    }
}
