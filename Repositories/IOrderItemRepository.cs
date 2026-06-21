using Entities;

namespace Repositories;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(int orderId, int productId);
    Task AddAsync(OrderItem entity);
    Task UpdateAsync(OrderItem entity);
    Task DeleteAsync(int orderId, int productId);
}
