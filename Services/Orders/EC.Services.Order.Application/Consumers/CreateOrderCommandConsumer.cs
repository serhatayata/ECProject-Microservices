using AutoMapper;
using Core.Extensions;
using Core.Utilities.Business.Abstract;
using Core.Utilities.Results;
using EC.Services.Order.Application.Commands;
using EC.Services.Order.Application.Dtos;
using EC.Services.Order.Infrastructure;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Application.Consumers
{
    public class CreateOrderCommandConsumer : IConsumer<OrderCreateDto>
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CreateOrderCommandConsumer(OrderDbContext orderDbContext, ISharedIdentityService sharedIdentityService)
        {
            _orderDbContext = orderDbContext;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task Consume(ConsumeContext<OrderCreateDto> context)
        {
            var userId = _sharedIdentityService.GetUserId;

            var order = new Domain.OrderAggregate.Order(userId, context.Message.Address);

            context.Message.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(x.ProductId, x.Price,x.Quantity,x.OrderId);
            });

            await _orderDbContext.Orders.AddAsync(order);

            await _orderDbContext.SaveChangesAsync();
        }

    }
}
