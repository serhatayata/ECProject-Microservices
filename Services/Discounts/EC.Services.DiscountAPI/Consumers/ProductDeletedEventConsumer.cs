using AutoMapper;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.DataAccess.Queue;
using Core.Dtos;
using Core.Utilities.Business.Abstract;
using EC.Services.DiscountAPI.Data.Abstract;
using EC.Services.DiscountAPI.Repositories.Abstract;
using EC.Services.DiscountAPI.Repositories.Concrete;
using MassTransit;

namespace EC.Services.DiscountAPI.Consumers
{
    public class ProductDeletedEventConsumer : IConsumer<ProductDeletedEvent>
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public ProductDeletedEventConsumer(ICampaignRepository campaignRepository,IDiscountRepository discountRepository , IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ProductDeletedEvent> context)
        {
            string productId = context.Message.ProductId;

            var result = await _campaignRepository.DeleteAllProductsAsync(new DeleteStringDto() { Id = productId });
        }

    }

    public class MessageFaultConsumer : IConsumer<Fault<ProductDeletedEvent>>
    {
        public async Task Consume(ConsumeContext<Fault<ProductDeletedEvent>> context)
        {
            //DO SOMETHING IF CONSUMING FAILS
            var error = $"Consuming fault: {context.Message.Exceptions[0].Message.ToString()}";
        }
    }

}
