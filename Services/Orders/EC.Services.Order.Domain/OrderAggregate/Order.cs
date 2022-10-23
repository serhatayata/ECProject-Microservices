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
            _orderItems = new List<OrderItem>();
            CDate = DateTime.Now;
            BuyerId = buyerId;
            CountryName = address.CountryName;
            CountyName = address.CountyName;
            CityName = address.CityName;
            AddressDetail = address.AddressDetail;
            ZipCode = address.ZipCode;
        }

        public string? BuyerId { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;


        public DateTime CDate { get; set; }

        public string CountryName { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public string AddressDetail { get; set; }
        public string ZipCode { get; set; }

        public void AddOrderItem(string productId, string productName, decimal price)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);

            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, price);

                _orderItems.Add(newOrderItem);
            }
        }
    }
}
