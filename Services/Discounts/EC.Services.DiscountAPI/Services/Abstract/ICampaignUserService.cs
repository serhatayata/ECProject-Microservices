using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignUser;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Services.Abstract
{
    public interface ICampaignUserService
    {
        Task<IResult> DeleteAsync(CampaignUserDeleteDto model);
        Task<DataResult<CampaignUserDto>> AddAsync(CampaignUserAddDto model);
        Task<DataResult<CampaignDto>> GetByCodeAsync(CampaignUserCodeDto model);
        Task<DataResult<List<CampaignDto>>> GetAllByCampaignIdAsync(CampaignIdDto model);
        Task<DataResult<List<CampaignDto>>> GetAllByCampaignIdPagingAsync(CampaignIdPagingDto model);
        Task<DataResult<List<CampaignDto>>> GetAllByUserIdAsync(CampaignUserIdDto model);
    }
}
