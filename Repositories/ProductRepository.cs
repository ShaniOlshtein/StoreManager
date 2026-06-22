using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StoreDbContext _context;

    public ProductRepository(StoreDbContext context) => _context = context;

    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _context.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(int id) =>
        await _context.Products.FindAsync(id);

    public async Task AddAsync(Product entity)
    {
        await _context.Products.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product entity)
    {
        _context.Products.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Products.FindAsync(id);
        if (entity is not null)
        {
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Product>> GetAllWithCategoryAsync() =>
        await _context.Products.Include(p => p.Category).ToListAsync();

    public async Task<Product?> GetByIdWithCategoryAsync(int id) =>
        await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
}
