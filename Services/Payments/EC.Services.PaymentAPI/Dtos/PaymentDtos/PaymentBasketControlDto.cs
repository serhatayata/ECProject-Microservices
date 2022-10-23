using Core.Entities;
using EC.Services.PaymentAPI.Dtos.BasketDtos;

namespace EC.Services.PaymentAPI.Dtos.PaymentDtos
{
    public class PaymentBasketControlDto:IDto
    {
        public BasketDto Basket { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
