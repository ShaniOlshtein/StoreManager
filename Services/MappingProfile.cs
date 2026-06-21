using AutoMapper;
using Common.DTOs;
using Entities;

namespace Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductReadDto>();
        CreateMap<ProductWriteDto, Product>();

        CreateMap<Category, CategoryReadDto>();
        CreateMap<CategoryWriteDto, Category>();

        CreateMap<Order, OrderReadDto>();
        CreateMap<OrderWriteDto, Order>();

        CreateMap<OrderItem, OrderItemReadDto>();
        CreateMap<OrderItemWriteDto, OrderItem>();
    }
}
