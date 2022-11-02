using EC.Services.PaymentAPI.Dtos.OrderDtos;

namespace EC.Services.PaymentAPI.Events
{
    public class OrderAddEvent
    {
        public string PaymentNo { get; set; }
        public AddressDto Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
