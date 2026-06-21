using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [Required, MaxLength(200)]
    public string CustomerName { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
