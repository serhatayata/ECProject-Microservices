using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Dtos.Discount;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Services.Abstract
{
    public interface IDiscountService : IBaseService<DiscountDto, DiscountAddDto, DiscountUpdateDto, DeleteIntDto>
    {
        Task<DataResult<DiscountDto>> GetDiscountByCodeAsync(DiscountGetByCodeDto model);
    }
}
