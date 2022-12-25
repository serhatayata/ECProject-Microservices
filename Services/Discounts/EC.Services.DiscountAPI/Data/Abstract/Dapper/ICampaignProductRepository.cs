using Core.Dtos;
using EC.Services.DiscountAPI.Entities;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Data.Abstract.Dapper
{
    public interface ICampaignProductRepository
    {
        Task<List<CampaignProduct>> GetAllByProductIdsAsync(int[] productIds);
        Task<IResult> DeleteAllProductsByCampaignIdAsync(DeleteIntDto model);
    }
}
