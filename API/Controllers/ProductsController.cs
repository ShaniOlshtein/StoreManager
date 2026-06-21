using API.DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly StoreDbContext _context;

    public ProductsController(StoreDbContext context)
    {
        _context = context;
    }

    // GET api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products.Select(ToReadDto));
    }

    // GET api/products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReadDto>> GetById(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product is null ? NotFound() : Ok(ToReadDto(product));
    }

    // POST api/products
    [HttpPost]
    public async Task<ActionResult<ProductReadDto>> Create(ProductWriteDto dto)
    {
        var product = ToEntity(dto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ToReadDto(product));
    }

    // PUT api/products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductWriteDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return NotFound();

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private static ProductReadDto ToReadDto(Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Price = p.Price,
        CategoryId = p.CategoryId
    };

    private static Product ToEntity(ProductWriteDto dto) => new()
    {
        Name = dto.Name,
        Price = dto.Price,
        CategoryId = dto.CategoryId
    };
}
