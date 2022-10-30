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
    public class GetOrderByIdQuery : IRequest<DataResult<OrderDto>>
    {
        public int Id { get; set; }
    }
}
