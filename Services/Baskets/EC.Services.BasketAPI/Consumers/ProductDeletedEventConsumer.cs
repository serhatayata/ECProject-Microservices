using Core.CrossCuttingConcerns.Caching.Redis;
using Core.DataAccess.Queue;
using Core.Utilities.Business.Abstract;
using EC.Services.BasketAPI.Constants;
using EC.Services.BasketAPI.Dtos;
using MassTransit;
using Newtonsoft.Json;

namespace EC.Services.BasketAPI.Consumers
{
    public class ProductDeletedEventConsumer : IConsumer<ProductDeletedEvent>
    {
        private readonly IRedisCacheManager _cacheManager;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly string _userId;

        public ProductDeletedEventConsumer(IRedisCacheManager cacheManager, ISharedIdentityService sharedIdentityService)
        {
            _cacheManager = cacheManager;
            _sharedIdentityService = sharedIdentityService;
            _userId = _sharedIdentityService.GetUserId;
        }

        public async Task Consume(ConsumeContext<ProductDeletedEvent> context)
        {
            var basketGet = await _cacheManager.GetDatabase(db: BasketTitles.BasketDb).StringGetAsync(_userId);

            if (string.IsNullOrEmpty(basketGet))
            {
                return;
            }

            var basket = JsonConvert.DeserializeObject<BasketDto>(basketGet.ToString());

            basket.basketItems.RemoveAll(x => x.ProductId == context.Message.ProductId);

        }
    }
}
