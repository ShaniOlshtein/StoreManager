using API.DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly StoreDbContext _context;

    public CategoriesController(StoreDbContext context)
    {
        _context = context;
    }

    // GET api/categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAll()
    {
        var categories = await _context.Categories.ToListAsync();
        return Ok(categories.Select(ToReadDto));
    }

    // GET api/categories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryReadDto>> GetById(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        return category is null ? NotFound() : Ok(ToReadDto(category));
    }

    // POST api/categories
    [HttpPost]
    public async Task<ActionResult<CategoryReadDto>> Create(CategoryWriteDto dto)
    {
        var category = new Category { Name = dto.Name };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, ToReadDto(category));
    }

    // PUT api/categories/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CategoryWriteDto dto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null) return NotFound();

        category.Name = dto.Name;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/categories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null) return NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private static CategoryReadDto ToReadDto(Category c) => new()
    {
        Id = c.Id,
        Name = c.Name
    };
}
