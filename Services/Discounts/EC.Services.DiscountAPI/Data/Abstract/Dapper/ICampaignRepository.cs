using Core.DataAccess;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Data.Abstract.Dapper
{
    public interface ICampaignRepository:IGenericRepository<Campaign>
    {
        Task<Campaign> GetWithStatusByIdAsync(int id, CampaignStatus status = CampaignStatus.Active);
        Task<Campaign> GetWithStatusByCodeAsync(string code, CampaignStatus status = CampaignStatus.Active);
        Task<List<Campaign>> GetAllWithStatusAsync(CampaignStatus status = CampaignStatus.Active);
        Task<List<Campaign>> GetAllPagingAsync(int page = 1, int pageSize = 8, CampaignStatus status = CampaignStatus.Active);
        Task<List<Campaign>> GetAllByCampaignTypeAsync(int campaignType, CampaignStatus status=CampaignStatus.Active);
        Task<List<Campaign>> GetAllBySponsorAsync(string sponsor, CampaignStatus status = CampaignStatus.Active);
        Task<List<Campaign>> GetAllByProductIdAsync(string productId, CampaignStatus status = CampaignStatus.Active);
        Task<List<Campaign>> GetAllBetweenDatesByExpirationTimeAsync(DateTime bTime, DateTime eTime, CampaignStatus status = CampaignStatus.Active);
        Task<List<Campaign>> GetAllBetweenDatesByCreatedTimeAsync(DateTime bTime, DateTime eTime, CampaignStatus status = CampaignStatus.Active);
    }
}
