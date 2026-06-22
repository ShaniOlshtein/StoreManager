using AutoMapper;
using Common.DTOs;
using Entities;
using Repositories;

namespace Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryReadDto>> GetAllAsync()
    {
        var categories = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
    }

    public async Task<CategoryReadDto?> GetByIdAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        return category is null ? null : _mapper.Map<CategoryReadDto>(category);
    }

    public async Task<CategoryReadDto> CreateAsync(CategoryWriteDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        await _repo.AddAsync(category);
        return _mapper.Map<CategoryReadDto>(category);
    }

    public async Task UpdateAsync(int id, CategoryWriteDto dto)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category is null) return;
        _mapper.Map(dto, category);
        await _repo.UpdateAsync(category);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}
