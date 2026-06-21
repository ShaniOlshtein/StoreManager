using AutoMapper;
using Common.DTOs;
using Entities;
using Repositories;

namespace Services;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _repo;
    private readonly IMapper _mapper;

    public OrderItemService(IOrderItemRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderItemReadDto>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderItemReadDto>>(items);
    }

    public async Task<OrderItemReadDto?> GetByIdAsync(int orderId, int productId)
    {
        var item = await _repo.GetByIdAsync(orderId, productId);
        return item is null ? null : _mapper.Map<OrderItemReadDto>(item);
    }

    public async Task<OrderItemReadDto> CreateAsync(OrderItemWriteDto dto)
    {
        var item = _mapper.Map<OrderItem>(dto);
        await _repo.AddAsync(item);
        return _mapper.Map<OrderItemReadDto>(item);
    }

    public async Task UpdateAsync(int orderId, int productId, OrderItemWriteDto dto)
    {
        var item = await _repo.GetByIdAsync(orderId, productId);
        if (item is null) return;
        _mapper.Map(dto, item);
        await _repo.UpdateAsync(item);
    }

    public async Task DeleteAsync(int orderId, int productId) =>
        await _repo.DeleteAsync(orderId, productId);
}
