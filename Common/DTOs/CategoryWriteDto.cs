using System.ComponentModel.DataAnnotations;

namespace Common.DTOs;

public class CategoryWriteDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;
}
