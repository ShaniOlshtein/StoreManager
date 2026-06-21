using Common.DTOs;

namespace Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryReadDto>> GetAllAsync();
    Task<CategoryReadDto?> GetByIdAsync(int id);
    Task<CategoryReadDto> CreateAsync(CategoryWriteDto dto);
    Task UpdateAsync(int id, CategoryWriteDto dto);
    Task DeleteAsync(int id);
}
