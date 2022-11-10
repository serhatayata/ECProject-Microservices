using Core.Entities;
using EC.Services.PaymentAPI.Dtos.BasketDtos;
using EC.Services.PaymentAPI.Dtos.OrderDtos;

namespace EC.Services.PaymentAPI.Dtos.PaymentDtos
{
    public class PaymentBasketControlDto:IDto
    {
        public string? UserId { get; set; }
        public BasketDto Basket { get; set; }
        public AddressDto Address { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
