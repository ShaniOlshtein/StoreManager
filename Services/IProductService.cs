using Common.DTOs;

namespace Services;

public interface IProductService
{
    Task<IEnumerable<ProductReadDto>> GetAllAsync();
    Task<ProductReadDto?> GetByIdAsync(int id);
    Task<ProductReadDto> CreateAsync(ProductWriteDto dto);
    Task UpdateAsync(int id, ProductWriteDto dto);
    Task DeleteAsync(int id);
}
