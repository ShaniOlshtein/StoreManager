namespace Common.DTOs;

public class ProductWriteDto
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
