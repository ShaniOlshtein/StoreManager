using System.ComponentModel.DataAnnotations;

namespace Common.DTOs;

public class OrderWriteDto
{
    [Required]
    [MaxLength(200)]
    public string CustomerName { get; set; } = null!;

    public List<OrderItemWriteDto> Items { get; set; } = new();
}
