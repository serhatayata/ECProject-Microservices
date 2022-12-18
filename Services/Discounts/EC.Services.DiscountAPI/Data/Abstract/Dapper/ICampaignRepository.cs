using Core.DataAccess.Dapper;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Data.Abstract.Dapper
{
    public interface ICampaignRepository:IGenericRepository<Campaign>
    {
        Task<List<Campaign>> GetAllPagingAsync(int page = 1, int pageSize = 8);
        Task<List<Campaign>> GetAllByProductIdAsync(int productId);
        Task<List<Campaign>> GetAllByCampaignTypeAsync(int campaignType);
        Task<List<Campaign>> GetAllBySponsorAsync(string sponsor);
        Task<List<Campaign>> GetAllBetweenDatesByExpirationTimeAsync(DateTime bTime, DateTime eTime);
        Task<List<Campaign>> GetAllBetweenDatesByCreatedTimeAsync(DateTime bTime, DateTime eTime);
    }
}
