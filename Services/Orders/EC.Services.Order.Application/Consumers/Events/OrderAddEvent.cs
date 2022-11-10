using EC.Services.Order.Application.Dtos;
using EC.Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Application.Consumers.Events
{
    public class OrderAddEvent
    {
        public string PaymentNo { get; set; }
        public Address Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
