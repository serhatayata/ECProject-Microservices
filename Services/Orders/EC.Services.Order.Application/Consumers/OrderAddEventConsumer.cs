using AutoMapper;
using Core.Utilities.Business.Abstract;
using EC.Services.Order.Application.Commands;
using EC.Services.Order.Application.Consumers.Events;
using EC.Services.Order.Application.Dtos;
using MassTransit;
using MediatR;

namespace EC.Services.Order.Application.Consumers
{
    public class OrderAddEventConsumer : IConsumer<OrderAddEvent>
    {
        private readonly IMapper _mapper;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly IMediator _mediator;

        public OrderAddEventConsumer(IMapper mapper,ISharedIdentityService sharedIdentityService,IMediator mediator)
        {
            _mapper = mapper;
            _sharedIdentityService = sharedIdentityService;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderAddEvent> context)
        {
            var userId = _sharedIdentityService.GetUserId;

            var createOrderCommand = _mapper.Map<CreateOrderCommand>(context.Message);
            createOrderCommand.UserId = userId;
            var response = await _mediator.Send(createOrderCommand); 

            //Return value will be researched later.
        }

    }
}
