using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly StoreDbContext _context;

    public CategoryRepository(StoreDbContext context) => _context = context;

    public async Task<IEnumerable<Category>> GetAllAsync() =>
        await _context.Categories.ToListAsync();

    public async Task<Category?> GetByIdAsync(int id) =>
        await _context.Categories.FindAsync(id);

    public async Task AddAsync(Category entity)
    {
        await _context.Categories.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category entity)
    {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Categories.FindAsync(id);
        if (entity is not null)
        {
            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
