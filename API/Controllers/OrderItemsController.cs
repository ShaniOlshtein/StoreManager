using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly IOrderItemService _service;

    public OrderItemsController(IOrderItemService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItemReadDto>>> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{orderId}/{productId}")]
    public async Task<ActionResult<OrderItemReadDto>> GetById(int orderId, int productId)
    {
        var item = await _service.GetByIdAsync(orderId, productId);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<OrderItemReadDto>> Create(OrderItemWriteDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { orderId = created.OrderId, productId = created.ProductId }, created);
    }

    [HttpPut("{orderId}/{productId}")]
    public async Task<IActionResult> Update(int orderId, int productId, OrderItemWriteDto dto)
    {
        await _service.UpdateAsync(orderId, productId, dto);
        return NoContent();
    }

    [HttpDelete("{orderId}/{productId}")]
    public async Task<IActionResult> Delete(int orderId, int productId)
    {
        await _service.DeleteAsync(orderId, productId);
        return NoContent();
    }
}
