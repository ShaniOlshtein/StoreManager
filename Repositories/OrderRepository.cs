using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(StoreDbContext context) : base(context) { }

    public async Task<IEnumerable<Order>> GetAllWithItemsAsync() =>
        await _dbSet.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();

    public async Task<Order?> GetByIdWithItemsAsync(int id) =>
        await _dbSet.Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.Id == id);
}
