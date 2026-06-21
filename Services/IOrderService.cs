using Common.DTOs;

namespace Services;

public interface IOrderService
{
    Task<IEnumerable<OrderReadDto>> GetAllAsync();
    Task<OrderReadDto?> GetByIdAsync(int id);
    Task<OrderReadDto> CreateAsync(OrderWriteDto dto);
    Task UpdateAsync(int id, OrderWriteDto dto);
    Task DeleteAsync(int id);
}
