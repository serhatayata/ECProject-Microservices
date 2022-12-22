using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Data.Abstract.Dapper
{
    public interface ICampaignProductRepository
    {
        Task<List<CampaignProduct>> GetAllByProductIdsAsync(int[] productIds);
    }
}
