using EC.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Domain.OrderAggregate
{
    public class OrderItem:Entity
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public Decimal Price { get; private set; }

        public OrderItem()
        {
        }

        public OrderItem(string productId, string productName, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
        }

        public void UpdateOrderItem(string productName, decimal price)
        {
            ProductName = productName;
            Price = price;
        }

    }
}
