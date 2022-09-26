using Core.Utilities.Results;
using EC.Services.BasketAPI.Dtos;
using MassTransit;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.BasketAPI.Services.Abstract
{
    public interface IBasketService
    {
        Task<DataResult<BasketDto>> GetBasket(string userId);
        Task<IResult> SaveOrUpdate(BasketSaveOrUpdateDto basketDto);
        Task<IResult> Delete(string userId);
    }
}
