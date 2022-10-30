using Core.Utilities.Results;
using EC.Services.Order.Application.Dtos;
using EC.Services.Order.Domain.OrderAggregate;
using MassTransit;
using MediatR;

namespace EC.Services.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<DataResult<CreatedOrderDto>>
    {
        public string UserId { get; set; }
        public Address Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }

    }
}
