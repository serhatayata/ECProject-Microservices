using Core.Entities;
using Core.Messages;
using EC.Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Application.Dtos
{
    public class OrderCreateDto:IDto
    {
        public string PaymentNo { get; set; }
        public AddressDto Address { get; set; }
        public List<OrderItemCreateDto> OrderItems { get; set; }
    }
}
