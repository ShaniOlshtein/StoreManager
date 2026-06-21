using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ICategoryService service, ILogger<CategoriesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAll()
    {
        _logger.LogInformation("GET all categories");
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryReadDto>> GetById(int id)
    {
        _logger.LogInformation("GET category {Id}", id);
        var category = await _service.GetByIdAsync(id);
        if (category is null)
        {
            _logger.LogWarning("Category {Id} not found", id);
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryReadDto>> Create(CategoryWriteDto dto)
    {
        var created = await _service.CreateAsync(dto);
        _logger.LogInformation("Created category {Id}", created.Id);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CategoryWriteDto dto)
    {
        _logger.LogInformation("UPDATE category {Id}", id);
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("DELETE category {Id}", id);
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
