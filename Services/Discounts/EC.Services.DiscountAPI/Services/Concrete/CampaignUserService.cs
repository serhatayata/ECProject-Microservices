using Core.Utilities.Results;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignUser;
using EC.Services.DiscountAPI.Services.Abstract;

namespace EC.Services.DiscountAPI.Services.Concrete
{
    public class CampaignUserService : ICampaignUserService
    {
        public Task<DataResult<CampaignUserDto>> AddAsync(CampaignUserAddDto model)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Utilities.Results.IResult> DeleteAsync(CampaignUserDeleteDto model)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<List<CampaignDto>>> GetAllByCampaignIdAsync(int campaignId, bool isUsed)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<List<CampaignDto>>> GetAllByCampaignIdPagingAsync(CampaignIdPagingDto model)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<List<CampaignDto>>> GetAllByUserIdAsync(string userId, bool isUsed)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<CampaignDto>> GetByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}
