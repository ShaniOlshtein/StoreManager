using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly StoreDbContext _context;

    public OrderRepository(StoreDbContext context) => _context = context;

    public async Task<IEnumerable<Order>> GetAllAsync() =>
        await _context.Orders.ToListAsync();

    public async Task<Order?> GetByIdAsync(int id) =>
        await _context.Orders.FindAsync(id);

    public async Task AddAsync(Order entity)
    {
        await _context.Orders.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order entity)
    {
        _context.Orders.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Orders.FindAsync(id);
        if (entity is not null)
        {
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Order>> GetAllWithItemsAsync() =>
        await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();

    public async Task<Order?> GetByIdWithItemsAsync(int id) =>
        await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                      .FirstOrDefaultAsync(o => o.Id == id);
}
