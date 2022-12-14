using AutoMapper;
using EC.Services.PaymentAPI.Dtos.BasketDtos;
using EC.Services.PaymentAPI.Dtos.OrderDtos;
using EC.Services.PaymentAPI.Dtos.PaymentDtos;
using EC.Services.PaymentAPI.Entities;

namespace EC.Services.PaymentAPI.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Payment
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Payment, PaymentAddDto>().ReverseMap();
            CreateMap<Payment, PaymentWithoutUserAddDto>().ReverseMap();
            CreateMap<PaymentAddDto, PaymentWithoutUserAddDto>().ReverseMap();

            CreateMap<OrderItemDto, BasketItemDto>().ReverseMap();
            CreateMap<Core.Messages.OrderItemDto, BasketItemDto>().ReverseMap();

            #endregion
        }
    }
}
