using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.Discount;
using EC.Services.DiscountAPI.Entities;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Services.Abstract
{
    public interface IDiscountService : IBaseService<DiscountDto, DiscountAddDto, DiscountUpdateDto, DeleteIntDto>
    {
        Task<IResult> ActivateDiscountAsync(DiscountIdDto model);
        Task<DataResult<DiscountDto>> GetAsync(int id);
        Task<DataResult<DiscountDto>> GetWithStatusByIdAsync(DiscountIdDto model);
        Task<DataResult<List<DiscountDto>>> GetAllWithStatusAsync(DiscountStatus status=DiscountStatus.Active);
        Task<DataResult<DiscountDto>> GetDiscountByCodeAsync(DiscountGetByCodeDto model);
    }
}
