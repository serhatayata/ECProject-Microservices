using AutoMapper;
using AutoMapper.Internal.Mappers;
using Core.Utilities.Results;
using EC.Services.Order.Application.Dtos;
using EC.Services.Order.Application.Queries;
using EC.Services.Order.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, DataResult<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;
        private readonly IMapper _mapper;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataResult<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.UserId == request.UserId).ToListAsync();

            if (!orders.Any())
            {
                return new SuccessDataResult<List<OrderDto>>(new List<OrderDto>());
            }

            var ordersDto = _mapper.Map<List<OrderDto>>(orders);

            return new SuccessDataResult<List<OrderDto>>(ordersDto);
        }
    }
}
