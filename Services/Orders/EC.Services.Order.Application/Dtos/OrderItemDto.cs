using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Application.Dtos
{
    public class OrderItemDto:IDto
    {
        public string ProductId { get; private set; }
        public Decimal Price { get; private set; }
        public int OrderId { get; set; }


    }
}
