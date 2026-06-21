using Entities;
using Repositories;

namespace Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;

    public OrderService(IOrderRepository repo) => _repo = repo;

    public async Task<IEnumerable<Order>> GetAllAsync() => await _repo.GetAllAsync();

    public async Task<Order?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

    public async Task<Order> CreateAsync(Order order)
    {
        await _repo.AddAsync(order);
        return order;
    }

    public async Task UpdateAsync(int id, Order order) => await _repo.UpdateAsync(order);

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}
