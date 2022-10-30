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
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, DataResult<OrderDto>>
    {
        private readonly OrderDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDapperOrderRepository _dapperOrderRepository;

        public GetOrderByIdQueryHandler(OrderDbContext context, IDapperOrderRepository dapperOrderRepository, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dapperOrderRepository = dapperOrderRepository;
        }

        public async Task<DataResult<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _dapperOrderRepository.GetByIdAsync(request.Id);

            if (order == null)
            {
                return new SuccessDataResult<OrderDto>();
            }

            var orderDto = _mapper.Map<OrderDto>(order);

            return new SuccessDataResult<OrderDto>(orderDto);
        }

    }
}
