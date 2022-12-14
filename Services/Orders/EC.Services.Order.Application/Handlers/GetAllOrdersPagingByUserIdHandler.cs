using AutoMapper;
using Core.Utilities.Results;
using EC.Services.Order.Application.Data.Abstract.Dapper;
using EC.Services.Order.Application.Dtos;
using EC.Services.Order.Application.Queries;
using EC.Services.Order.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Application.Handlers
{
    public class GetAllOrdersPagingByUserIdHandler : IRequestHandler<GetAllOrdersPagingByUserIdQuery, DataResult<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDapperOrderRepository _dapperOrderRepository;

        public GetAllOrdersPagingByUserIdHandler(OrderDbContext context, IDapperOrderRepository dapperOrderRepository, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dapperOrderRepository = dapperOrderRepository;
        }

        public async Task<DataResult<List<OrderDto>>> Handle(GetAllOrdersPagingByUserIdQuery request, CancellationToken cancellationToken)
        {
            //var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.UserId == request.UserId).ToListAsync();
            var orders = await _dapperOrderRepository.GetAllPagingByUserIdAsync(request.UserId,request.Page,request.PageSize);

            if (!orders.Any())
            {
                return new SuccessDataResult<List<OrderDto>>(new List<OrderDto>());
            }

            var ordersDto = _mapper.Map<List<OrderDto>>(orders);

            return new SuccessDataResult<List<OrderDto>>(ordersDto);
        }


    }
}
