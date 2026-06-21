using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService service, ILogger<ProductsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll()
    {
        _logger.LogInformation("GET all products");
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReadDto>> GetById(int id)
    {
        _logger.LogInformation("GET product {Id}", id);
        var product = await _service.GetByIdAsync(id);
        if (product is null)
        {
            _logger.LogWarning("Product {Id} not found", id);
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductReadDto>> Create(ProductWriteDto dto)
    {
        var created = await _service.CreateAsync(dto);
        _logger.LogInformation("Created product {Id}", created.Id);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductWriteDto dto)
    {
        _logger.LogInformation("UPDATE product {Id}", id);
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("DELETE product {Id}", id);
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
