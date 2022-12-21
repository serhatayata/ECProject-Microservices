using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Dtos.Campaign;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Services.Abstract
{
    public interface ICampaignService : IBaseService<CampaignDto, CampaignAddDto, CampaignUpdateDto, DeleteIntDto>
    {
        Task<IResult> AddProductsAsync(CampaignAddProductsDto model);
        Task<IResult> DeleteProductAsync(CampaignDeleteProductDto model);
        Task<IResult> DeleteAllProductsByCampaignIdAsync(DeleteIntDto model);
        Task<DataResult<List<CampaignDto>>> GetProductCampaignsAsync(int productId);


    }
}
