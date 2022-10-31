using Core.Entities;

namespace EC.Services.PaymentAPI.Dtos.OrderDtos
{
    public class OrderItemDto:IDto
    {
        public string ProductId { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
