using Core.DataAccess;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignUser;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Data.Abstract.Dapper
{
    public interface ICampaignUserRepository : IGenericRepository<CampaignUser>
    {
        Task<CampaignUser> GetByCodeAsync(CampaignUserCodeDto model);
        Task<List<CampaignUser>> GetAllByCampaignIdAsync(int campaignId, bool isUsed);
        Task<List<CampaignUser>> GetAllByCampaignIdPagingAsync(CampaignIdPagingDto model);
        Task<List<CampaignUser>> GetAllByUserIdAsync(string userId, bool isUsed);

    }
}
