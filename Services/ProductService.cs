using AutoMapper;
using Common.DTOs;
using Entities;
using Repositories;

namespace Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
    {
        var products = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductReadDto>>(products);
    }

    public async Task<ProductReadDto?> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        return product is null ? null : _mapper.Map<ProductReadDto>(product);
    }

    public async Task<ProductReadDto> CreateAsync(ProductWriteDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _repo.AddAsync(product);
        return _mapper.Map<ProductReadDto>(product);
    }

    public async Task UpdateAsync(int id, ProductWriteDto dto)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product is null) return;
        _mapper.Map(dto, product);
        await _repo.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}
