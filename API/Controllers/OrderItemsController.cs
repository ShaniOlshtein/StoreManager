using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly StoreDbContext _context;

    public OrderItemsController(StoreDbContext context)
    {
        _context = context;
    }

    // GET api/orderitems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetAll()
    {
        return await _context.OrderItems.Include(oi => oi.Product).Include(oi => oi.Order).ToListAsync();
    }

    // GET api/orderitems/5/3  (orderId/productId)
    [HttpGet("{orderId}/{productId}")]
    public async Task<ActionResult<OrderItem>> GetById(int orderId, int productId)
    {
        var item = await _context.OrderItems.Include(oi => oi.Product).Include(oi => oi.Order)
            .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.ProductId == productId);
        return item is null ? NotFound() : Ok(item);
    }

    // POST api/orderitems
    [HttpPost]
    public async Task<ActionResult<OrderItem>> Create(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { orderId = orderItem.OrderId, productId = orderItem.ProductId }, orderItem);
    }

    // PUT api/orderitems/5/3
    [HttpPut("{orderId}/{productId}")]
    public async Task<IActionResult> Update(int orderId, int productId, OrderItem orderItem)
    {
        if (orderId != orderItem.OrderId || productId != orderItem.ProductId) return BadRequest();
        _context.Entry(orderItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.OrderItems.AnyAsync(oi => oi.OrderId == orderId && oi.ProductId == productId)) return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE api/orderitems/5/3
    [HttpDelete("{orderId}/{productId}")]
    public async Task<IActionResult> Delete(int orderId, int productId)
    {
        var item = await _context.OrderItems.FindAsync(orderId, productId);
        if (item is null) return NotFound();

        _context.OrderItems.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
