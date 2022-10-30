using Core.Entities;
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
        public Address Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
