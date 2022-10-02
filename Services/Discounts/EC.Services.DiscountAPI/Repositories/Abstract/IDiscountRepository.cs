using Core.Utilities.Results;
using EC.Services.DiscountAPI.Dtos.Discount;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Repositories.Abstract
{
    public interface IDiscountRepository : IBaseRepository<DiscountDto, DiscountAddDto, DiscountUpdateDto>
    {
        Task<DataResult<DiscountDto>> GetDiscountByCodeAsync(string code);
    }
}
