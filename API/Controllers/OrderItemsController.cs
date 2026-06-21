using API.DTOs;
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
    public async Task<ActionResult<IEnumerable<OrderItemReadDto>>> GetAll()
    {
        var items = await _context.OrderItems.ToListAsync();
        return Ok(items.Select(ToReadDto));
    }

    // GET api/orderitems/5/3 (orderId/productId)
    [HttpGet("{orderId}/{productId}")]
    public async Task<ActionResult<OrderItemReadDto>> GetById(int orderId, int productId)
    {
        var item = await _context.OrderItems.FindAsync(orderId, productId);
        return item is null ? NotFound() : Ok(ToReadDto(item));
    }

    // POST api/orderitems
    [HttpPost]
    public async Task<ActionResult<OrderItemReadDto>> Create(OrderItemWriteDto dto)
    {
        var item = ToEntity(dto);
        _context.OrderItems.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { orderId = item.OrderId, productId = item.ProductId }, ToReadDto(item));
    }

    // PUT api/orderitems/5/3
    [HttpPut("{orderId}/{productId}")]
    public async Task<IActionResult> Update(int orderId, int productId, OrderItemWriteDto dto)
    {
        var item = await _context.OrderItems.FindAsync(orderId, productId);
        if (item is null) return NotFound();

        item.Quantity = dto.Quantity;
        item.UnitPrice = dto.UnitPrice;
        await _context.SaveChangesAsync();
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

    private static OrderItemReadDto ToReadDto(OrderItem oi) => new()
    {
        OrderId = oi.OrderId,
        ProductId = oi.ProductId,
        Quantity = oi.Quantity,
        UnitPrice = oi.UnitPrice
    };

    private static OrderItem ToEntity(OrderItemWriteDto dto) => new()
    {
        OrderId = dto.OrderId,
        ProductId = dto.ProductId,
        Quantity = dto.Quantity,
        UnitPrice = dto.UnitPrice
    };
}
