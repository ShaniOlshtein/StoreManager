using Entities;
using Repositories;

namespace Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo) => _repo = repo;

    public async Task<IEnumerable<Product>> GetAllAsync() => await _repo.GetAllAsync();

    public async Task<Product?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

    public async Task<Product> CreateAsync(Product product)
    {
        await _repo.AddAsync(product);
        return product;
    }

    public async Task UpdateAsync(int id, Product product) => await _repo.UpdateAsync(product);

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}
