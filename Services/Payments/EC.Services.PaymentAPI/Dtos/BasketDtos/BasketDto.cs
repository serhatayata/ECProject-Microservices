using Core.Entities;

namespace EC.Services.PaymentAPI.Dtos.BasketDtos
{
    public class BasketDto:IDto
    {
        public string? UserId { get; set; }
        public string? DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        public List<BasketItemDto> basketItems { get; set; }
    }
}
