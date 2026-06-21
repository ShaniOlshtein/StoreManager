using Entities;

namespace Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetAllWithItemsAsync();
    Task<Order?> GetByIdWithItemsAsync(int id);
}
