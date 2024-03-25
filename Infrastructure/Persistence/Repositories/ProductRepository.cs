using Application.Products;
using Domain.Entities.Products;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class ProductRepository(ApplicationDbContext _context) : IProductRepository
{
    public void Add(Product product)
    {
        _context.Add(product);
    }
    public async Task<Product?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == Id, cancellationToken);
    }
    public async Task<Result<IReadOnlyCollection<Product>>> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products.OrderBy(p => p.Name).ToListAsync(cancellationToken);
    }
    public void Update(Product product)
    {
        _context.Products.Update(product);
    }
    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }
}
