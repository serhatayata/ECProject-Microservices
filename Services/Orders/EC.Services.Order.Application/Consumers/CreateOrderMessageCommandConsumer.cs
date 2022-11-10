using AutoMapper;
using Core.Messages;
using Core.Utilities.Business.Abstract;
using EC.Services.Order.Application.Commands;
using MassTransit;
using MediatR;

namespace EC.Services.Order.Application.Consumers
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        private readonly IMapper _mapper;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly IMediator _mediator;

        public CreateOrderMessageCommandConsumer(IMapper mapper,ISharedIdentityService sharedIdentityService,IMediator mediator)
        {
            _mapper = mapper;
            _sharedIdentityService = sharedIdentityService;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            //var userId = _sharedIdentityService.GetUserId;
            var createOrderCommand = _mapper.Map<CreateOrderCommand>(context.Message);
            var response = await _mediator.Send(createOrderCommand);

            //Return value will be researched later.
        }

    }
}
