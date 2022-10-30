using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Application.Dtos
{
    public class CreatedOrderDto:IDto
    {
        public string OrderNo { get; set; }
    }
}
