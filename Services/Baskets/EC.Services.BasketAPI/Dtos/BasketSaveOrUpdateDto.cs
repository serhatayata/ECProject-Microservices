using Core.Entities;

namespace EC.Services.BasketAPI.Dtos
{
    public class BasketSaveOrUpdateDto:IDto
    {
        public string? DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        public List<BasketItemDto> basketItems { get; set; }
        public decimal TotalPrice
        {
            get => basketItems.Sum(x => x.Price * x.Quantity);
        }
    }
}
