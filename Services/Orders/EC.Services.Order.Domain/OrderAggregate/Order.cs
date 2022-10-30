using Core.Extensions;
using EC.Services.Order.Domain.Core;

namespace EC.Services.Order.Domain.OrderAggregate
{
    public class Order:Entity,IAggregateRoot
    {
        public Order()
        {

        }

        public Order(string buyerId, Address address)
        {
            OrderItems = new List<OrderItem>();
            CDate = DateTime.Now;
            UserId = buyerId;
            CountryName = address.CountryName;
            CountyName = address.CountyName;
            CityName = address.CityName;
            AddressDetail = address.AddressDetail;
            ZipCode = address.ZipCode;

            OrderNo= RandomExtensions.RandomString(12);
        }

        public ICollection<OrderItem> OrderItems { get; set; }

        public string? UserId { get; set; }

        public DateTime CDate { get; set; }
        public string OrderNo { get; set; }

        public string CountryName { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public string AddressDetail { get; set; }
        public string ZipCode { get; set; }

        public void AddOrderItem(string productId, decimal price, int quantity, int orderId)
        {
            var existProduct = OrderItems.Any(x => x.ProductId == productId);

            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, price,quantity,orderId);

                OrderItems.Add(newOrderItem);
            }
        }
    }
}
