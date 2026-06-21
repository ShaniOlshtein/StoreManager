using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly StoreDbContext _context;

    public OrderItemRepository(StoreDbContext context) => _context = context;

    public async Task<IEnumerable<OrderItem>> GetAllAsync() =>
        await _context.OrderItems.Include(oi => oi.Product).Include(oi => oi.Order).ToListAsync();

    public async Task<OrderItem?> GetByIdAsync(int orderId, int productId) =>
        await _context.OrderItems.Include(oi => oi.Product).Include(oi => oi.Order)
                      .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.ProductId == productId);

    public async Task AddAsync(OrderItem entity)
    {
        await _context.OrderItems.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderItem entity)
    {
        _context.OrderItems.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int orderId, int productId)
    {
        var item = await _context.OrderItems.FindAsync(orderId, productId);
        if (item is not null)
        {
            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
