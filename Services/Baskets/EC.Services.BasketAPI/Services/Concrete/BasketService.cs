using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.BasketAPI.Constants;
using EC.Services.BasketAPI.Dtos;
using EC.Services.BasketAPI.Services.Abstract;
using System.Text.Json;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.BasketAPI.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly IRedisCacheManager _cacheManager;

        public BasketService(IRedisCacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        #region SaveOrUpdate
        public async Task<IResult> SaveOrUpdate(BasketSaveOrUpdateDto basketDto)
        {
            bool status;
            status = await _cacheManager.GetDatabase(db: BasketTitles.BasketDb).StringSetAsync("basket_" + basketDto.UserId, JsonSerializer.Serialize(basketDto));

            return status ? new SuccessResult(MessageExtensions.SavedOrUpdated(BasketTitles.Basket)) : new ErrorResult(MessageExtensions.NotSavedOrUpdated(BasketTitles.Basket), StatusCodes.Status500InternalServerError);
        }
        #endregion
        #region GetBasket
        public async Task<DataResult<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await _cacheManager.GetDatabase(db:BasketTitles.BasketDb).StringGetAsync("basket_"+userId);

            if (String.IsNullOrEmpty(existBasket))
            {
                return new ErrorDataResult<BasketDto>(MessageExtensions.NotFound(BasketTitles.Basket),StatusCodes.Status200OK);
            }
            var basket = JsonSerializer.Deserialize<BasketDto>(existBasket);
            basket.UserId = userId;
            return new SuccessDataResult<BasketDto>(basket);
        }
        #endregion
        #region Delete
        public async Task<IResult> Delete(string userId)
        {
            var status = await _cacheManager.GetDatabase(db:BasketTitles.BasketDb).KeyDeleteAsync("basket_"+userId);

            return status ? new SuccessDataResult<bool>(true) : new ErrorDataResult<bool>("Basket not found", 404);
        }
        #endregion

    }
}
