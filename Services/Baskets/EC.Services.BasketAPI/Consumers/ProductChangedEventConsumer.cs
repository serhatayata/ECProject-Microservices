using Core.CrossCuttingConcerns.Caching.Redis;
using Core.DataAccess.Queue;
using Core.Utilities.Business.Abstract;
using EC.Services.BasketAPI.Constants;
using EC.Services.BasketAPI.Dtos;
using MassTransit;
using Newtonsoft.Json;

namespace EC.Services.BasketAPI.Consumers
{
    public class ProductChangedEventConsumer:IConsumer<ProductChangedEvent>
    {
        private readonly IRedisCacheManager _cacheManager;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly string _userId;

        public ProductChangedEventConsumer(IRedisCacheManager cacheManager, ISharedIdentityService sharedIdentityService)
        {
            _cacheManager = cacheManager;
            _sharedIdentityService = sharedIdentityService;
            _userId = _sharedIdentityService.GetUserId;
        }

        public async Task Consume(ConsumeContext<ProductChangedEvent> context)
        {
            var basketGet = await _cacheManager.GetDatabase(db: BasketTitles.BasketDb).StringGetAsync(_userId);

            if (string.IsNullOrEmpty(basketGet))
            {
                return;
            }

            var basket = JsonConvert.DeserializeObject<BasketDto>(basketGet.ToString());

            basket.basketItems.ForEach(x =>
            {
                if (x.ProductId == context.Message.ProductId)
                {
                    x.ProductName = context.Message.ProductName;
                    x.Price = context.Message.Price;
                }
            });

            await _cacheManager.GetDatabase(db: BasketTitles.BasketDb).StringSetAsync(_userId, JsonConvert.SerializeObject(basket));

        }
    }
}
