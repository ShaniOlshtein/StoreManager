using Common.DTOs;

namespace Services;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItemReadDto>> GetAllAsync();
    Task<OrderItemReadDto?> GetByIdAsync(int orderId, int productId);
    Task<OrderItemReadDto> CreateAsync(OrderItemWriteDto dto);
    Task UpdateAsync(int orderId, int productId, OrderItemWriteDto dto);
    Task DeleteAsync(int orderId, int productId);
}
