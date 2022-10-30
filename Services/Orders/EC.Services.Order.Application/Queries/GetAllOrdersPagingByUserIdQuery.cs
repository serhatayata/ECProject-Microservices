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
    public class GetAllOrdersPagingByUserIdQuery : IRequest<DataResult<List<OrderDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 8;
        public string UserId { get; set; }

    }
}
