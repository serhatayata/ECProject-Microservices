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
        public string ProductId { get; set; }
        //public string ProductName { get; private set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }

        public OrderItem()
        {
        }

        public OrderItem(string productId, decimal price, int quantity,int orderId)
        {
            ProductId = productId;
            Price = price;
            Quantity = quantity;
            OrderId = orderId;
        }

        public void UpdateOrderItem(decimal price)
        {
            Price = price;
        }

    }
}
