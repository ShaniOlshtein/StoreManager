using System.ComponentModel.DataAnnotations;

namespace Common.DTOs;

public class ProductWriteDto
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = null!;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
