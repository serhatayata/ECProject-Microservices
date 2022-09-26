using Core.Entities;

namespace EC.Services.BasketAPI.Dtos
{
    public class BasketItemDto:IDto
    {
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
