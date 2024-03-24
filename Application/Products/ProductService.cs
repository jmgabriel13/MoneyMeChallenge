using Domain.Entities.Product;

namespace Application.Products;
public sealed class ProductService(
    IProductRepository _productRepository) : IProductService
{
    public async Task<IReadOnlyCollection<Product>> GetAllProducts(CancellationToken cancellationToken = default)
    {
        return await _productRepository.GetAllProductsAsync(cancellationToken);
    }
}
