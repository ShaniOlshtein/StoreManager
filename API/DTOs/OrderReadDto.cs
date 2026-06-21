namespace API.DTOs;

public class OrderReadDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; } = null!;
}
