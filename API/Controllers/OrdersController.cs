using API.DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly StoreDbContext _context;

    public OrdersController(StoreDbContext context)
    {
        _context = context;
    }

    // GET api/orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAll()
    {
        var orders = await _context.Orders.ToListAsync();
        return Ok(orders.Select(ToReadDto));
    }

    // GET api/orders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderReadDto>> GetById(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        return order is null ? NotFound() : Ok(ToReadDto(order));
    }

    // POST api/orders
    [HttpPost]
    public async Task<ActionResult<OrderReadDto>> Create(OrderWriteDto dto)
    {
        var order = new Order { CustomerName = dto.CustomerName };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, ToReadDto(order));
    }

    // PUT api/orders/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, OrderWriteDto dto)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null) return NotFound();

        order.CustomerName = dto.CustomerName;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/orders/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null) return NotFound();

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private static OrderReadDto ToReadDto(Order o) => new()
    {
        Id = o.Id,
        OrderDate = o.OrderDate,
        CustomerName = o.CustomerName
    };
}
