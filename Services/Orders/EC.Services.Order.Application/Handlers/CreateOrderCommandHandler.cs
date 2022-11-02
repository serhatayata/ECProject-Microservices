using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.Order.Application.Commands;
using EC.Services.Order.Application.Dtos;
using EC.Services.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, DataResult<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<DataResult<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            string orderNo = RandomExtensions.RandomString(12);

            var orderExists = await _context.Orders.FirstOrDefaultAsync(x => x.OrderNo == orderNo );
            while(orderExists != null)
            {
                orderNo = RandomExtensions.RandomString(12);

                var orderExistsItem = await _context.Orders.FirstOrDefaultAsync(x => x.OrderNo == orderNo);
                if (orderExistsItem == null)
                {
                    break;
                }
            }

            Domain.OrderAggregate.Order newOrder = new(
                  request.UserId,request.PaymentNo, request.Address
                );

            newOrder.OrderNo = orderNo;

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.Price,x.Quantity,x.OrderId);
            });

            await _context.Orders.AddAsync(newOrder);

            await _context.SaveChangesAsync();

            var orderCheck = await _context.Orders.FirstOrDefaultAsync(x => x.Id == newOrder.Id);
            if (orderCheck?.Id == null)
            {
                return new ErrorDataResult<CreatedOrderDto>(MessageExtensions.NotCreated(OrderConstantValues.Order));
            }
            return new SuccessDataResult<CreatedOrderDto>(new CreatedOrderDto() { OrderNo= orderCheck.OrderNo });
        }
    }
}
