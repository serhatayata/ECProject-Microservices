using Core.Entities;
using EC.Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Application.Dtos
{
    public class OrderDto:IDto
    {
        public int Id { get; set; }
        public string? UserId { get; private set; }

        public DateTime CDate { get; set; }

        public string CountryName { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public string AddressDetail { get; set; }
        public string ZipCode { get; set; }

        private readonly List<OrderItem> _orderItems;
    }
}
