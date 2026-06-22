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

        CreateMap<Order, OrderReadDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));
        CreateMap<OrderWriteDto, Order>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items));

        CreateMap<OrderItem, OrderItemReadDto>();
        CreateMap<OrderItemWriteDto, OrderItem>();
    }
}
