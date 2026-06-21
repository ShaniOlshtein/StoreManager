using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(StoreDbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetAllWithCategoryAsync() =>
        await _dbSet.Include(p => p.Category).ToListAsync();

    public async Task<Product?> GetByIdWithCategoryAsync(int id) =>
        await _dbSet.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
}
