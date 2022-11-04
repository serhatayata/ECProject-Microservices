using AutoMapper;
using Core.Messages;
using EC.Services.Order.Application.Commands;
using EC.Services.Order.Application.Dtos;
using EC.Services.Order.Domain.OrderAggregate;

namespace EC.Services.Order.Application.Mapping
{
    public class CustomMapping:Profile
    {
        public CustomMapping()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<CreateOrderCommand, OrderCreateDto>().ReverseMap();
            CreateMap<OrderItem, Core.Messages.OrderItemDto>().ReverseMap();
            CreateMap<OrderItem, Dtos.OrderItemDto>().ReverseMap();
            CreateMap<Core.Messages.OrderItemDto, Dtos.OrderItemDto>().ReverseMap();
            CreateMap<Address, Core.Messages.AddressDto>().ReverseMap();
            CreateMap<CreateOrderCommand, CreateOrderMessageCommand>().ReverseMap();


        }

    }
}
