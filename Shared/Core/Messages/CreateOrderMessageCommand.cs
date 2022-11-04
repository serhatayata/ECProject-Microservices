using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages
{
    public class CreateOrderMessageCommand
    {
        public string UserId { get; set; }
        public string PaymentNo { get; set; }
        public AddressDto Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }

    }

    public class AddressDto
    {
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string CountyName { get; set; }
        public string AddressDetail { get; set; }
        public string ZipCode { get; set; }
    }

    public class OrderItemDto
    {
        public string ProductId { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }
}
