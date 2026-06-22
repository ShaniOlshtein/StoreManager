using AutoMapper;
using Common.DTOs;
using Entities;
using Repositories;

namespace Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderReadDto>> GetAllAsync()
    {
        var orders = await _repo.GetAllWithItemsAsync();
        return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
    }

    public async Task<OrderReadDto?> GetByIdAsync(int id)
    {
        var order = await _repo.GetByIdWithItemsAsync(id);
        return order is null ? null : _mapper.Map<OrderReadDto>(order);
    }

    public async Task<OrderReadDto> CreateAsync(OrderWriteDto dto)
    {
        var order = _mapper.Map<Order>(dto);
        await _repo.AddAsync(order);
        return _mapper.Map<OrderReadDto>(order);
    }

    public async Task UpdateAsync(int id, OrderWriteDto dto)
    {
        var order = await _repo.GetByIdAsync(id);
        if (order is null) return;
        _mapper.Map(dto, order);
        await _repo.UpdateAsync(order);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}
