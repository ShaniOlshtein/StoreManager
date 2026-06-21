using Entities;

namespace Services;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(int orderId, int productId);
    Task<OrderItem> CreateAsync(OrderItem orderItem);
    Task UpdateAsync(int orderId, int productId, OrderItem orderItem);
    Task DeleteAsync(int orderId, int productId);
}
