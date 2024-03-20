using Application.Products;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Product product)
    {
        _context.Add(product);
    }

    public async Task<Product?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == Id, cancellationToken);
    }
}
