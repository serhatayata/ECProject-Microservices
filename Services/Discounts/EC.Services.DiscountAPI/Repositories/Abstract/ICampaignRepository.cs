using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.Discount;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Repositories.Abstract
{
    public interface ICampaignRepository : IBaseRepository<CampaignDto, CampaignAddDto, CampaignUpdateDto>
    {
        Task<IResult> AddProductsAsync(CampaignAddProductsDto model);
        Task<IResult> DeleteProductAsync(CampaignDeleteProductDto model);
        Task<IResult> DeleteAllProductsAsync(DeleteStringDto model);
        Task<DataResult<List<CampaignDto>>> GetProductCampaignsAsync(string productId);

    }
}
