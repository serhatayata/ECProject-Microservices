using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignProduct;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Services.Abstract
{
    public interface ICampaignService : IBaseService<CampaignDto, CampaignAddDto, CampaignUpdateDto, DeleteIntDto>
    {
        Task<DataResult<CampaignDto>> GetAsync(int id);
        Task<DataResult<CampaignProductDto>> AddProductAsync(CampaignAddProductDto model);
        Task<IResult> DeleteProductAsync(CampaignDeleteProductDto model);
        Task<IResult> DeleteAllProductsByCampaignIdAsync(DeleteIntDto model);
        Task<DataResult<List<CampaignDto>>> GetProductCampaignsAsync(int productId);


    }
}
