using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly IOrderItemService _service;
    private readonly ILogger<OrderItemsController> _logger;

    public OrderItemsController(IOrderItemService service, ILogger<OrderItemsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItemReadDto>>> GetAll()
    {
        _logger.LogInformation("GET all order items");
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{orderId}/{productId}")]
    public async Task<ActionResult<OrderItemReadDto>> GetById(int orderId, int productId)
    {
        _logger.LogInformation("GET order item {OrderId}/{ProductId}", orderId, productId);
        var item = await _service.GetByIdAsync(orderId, productId);
        if (item is null)
        {
            _logger.LogWarning("Order item {OrderId}/{ProductId} not found", orderId, productId);
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<OrderItemReadDto>> Create(OrderItemWriteDto dto)
    {
        var created = await _service.CreateAsync(dto);
        _logger.LogInformation("Created order item {OrderId}/{ProductId}", created.OrderId, created.ProductId);
        return CreatedAtAction(nameof(GetById), new { orderId = created.OrderId, productId = created.ProductId }, created);
    }

    [HttpPut("{orderId}/{productId}")]
    public async Task<IActionResult> Update(int orderId, int productId, OrderItemWriteDto dto)
    {
        _logger.LogInformation("UPDATE order item {OrderId}/{ProductId}", orderId, productId);
        await _service.UpdateAsync(orderId, productId, dto);
        return NoContent();
    }

    [HttpDelete("{orderId}/{productId}")]
    public async Task<IActionResult> Delete(int orderId, int productId)
    {
        _logger.LogInformation("DELETE order item {OrderId}/{ProductId}", orderId, productId);
        await _service.DeleteAsync(orderId, productId);
        return NoContent();
    }
}
