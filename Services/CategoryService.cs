using Entities;
using Repositories;

namespace Services;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> _repo;

    public CategoryService(IGenericRepository<Category> repo) => _repo = repo;

    public async Task<IEnumerable<Category>> GetAllAsync() => await _repo.GetAllAsync();

    public async Task<Category?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

    public async Task<Category> CreateAsync(Category category)
    {
        await _repo.AddAsync(category);
        return category;
    }

    public async Task UpdateAsync(int id, Category category) => await _repo.UpdateAsync(category);

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}
