using Core.Extensions;
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
    public class CreateOrderCommandConsumer : IConsumer<CreateOrderCommand>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderCommandConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CreateOrderCommand> context)
        {
            var order = new Domain.OrderAggregate.Order(context.Message.BuyerId, context.Message.Address);

            context.Message.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(x.ProductId, x.Price);
            });

            await _orderDbContext.Orders.AddAsync(order);

            await _orderDbContext.SaveChangesAsync();
        }

    }
}
