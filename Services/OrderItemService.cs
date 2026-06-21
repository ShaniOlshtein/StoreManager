using Entities;
using Repositories;

namespace Services;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _repo;

    public OrderItemService(IOrderItemRepository repo) => _repo = repo;

    public async Task<IEnumerable<OrderItem>> GetAllAsync() => await _repo.GetAllAsync();

    public async Task<OrderItem?> GetByIdAsync(int orderId, int productId) =>
        await _repo.GetByIdAsync(orderId, productId);

    public async Task<OrderItem> CreateAsync(OrderItem orderItem)
    {
        await _repo.AddAsync(orderItem);
        return orderItem;
    }

    public async Task UpdateAsync(int orderId, int productId, OrderItem orderItem) =>
        await _repo.UpdateAsync(orderItem);

    public async Task DeleteAsync(int orderId, int productId) =>
        await _repo.DeleteAsync(orderId, productId);
}
