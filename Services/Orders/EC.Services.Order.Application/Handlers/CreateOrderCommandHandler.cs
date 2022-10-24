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
            Domain.OrderAggregate.Order newOrder = new (
                  request.BuyerId,request.Address
                );

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.Price);
            });

            await _context.Orders.AddAsync(newOrder);

            await _context.SaveChangesAsync();

            var orderExists = await _context.Orders.FirstOrDefaultAsync(x => x.Id == newOrder.Id);
            if (orderExists?.Id == null)
            {
                return new ErrorDataResult<CreatedOrderDto>(MessageExtensions.NotCreated(OrderConstantValues.Order));
            }
            return new SuccessDataResult<CreatedOrderDto>(new CreatedOrderDto() { OrderId=orderExists.Id });
        }
    }
}
