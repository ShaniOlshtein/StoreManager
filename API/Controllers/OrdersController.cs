using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService service, ILogger<OrdersController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAll()
    {
        _logger.LogInformation("GET all orders");
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderReadDto>> GetById(int id)
    {
        _logger.LogInformation("GET order {Id}", id);
        var order = await _service.GetByIdAsync(id);
        if (order is null)
        {
            _logger.LogWarning("Order {Id} not found", id);
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderReadDto>> Create(OrderWriteDto dto)
    {
        var created = await _service.CreateAsync(dto);
        _logger.LogInformation("Created order {Id}", created.Id);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, OrderWriteDto dto)
    {
        _logger.LogInformation("UPDATE order {Id}", id);
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("DELETE order {Id}", id);
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
