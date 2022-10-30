using Core.Utilities.Results;
using EC.Services.Order.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Application.Queries
{
    public class GetOrderByOrderNoQuery : IRequest<DataResult<OrderDto>>
    {
        public string UserId { get; set; }
        public string OrderNo { get; set; }


    }
}
