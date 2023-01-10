using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignProduct;
using EC.Services.DiscountAPI.Entities;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Services.Abstract
{
    public interface ICampaignService : IBaseService<CampaignDto, CampaignAddDto, CampaignUpdateDto, DeleteIntDto>
    {
        Task<DataResult<CampaignDto>> GetAsync(int id);
        Task<DataResult<CampaignDto>> GetWithStatusByIdAsync(int id, CampaignStatus status);
        Task<DataResult<List<CampaignDto>>> GetAllWithStatusAsync(CampaignStatus status);
        Task<DataResult<CampaignProductDto>> AddProductAsync(CampaignAddProductDto model);
        Task<IResult> DeleteProductAsync(CampaignDeleteProductDto model);
        Task<IResult> DeleteAllProductsByCampaignIdAsync(DeleteIntDto model);
        Task<IResult> ActivateCampaignAsync(DeleteIntDto model);
        Task<DataResult<List<CampaignDto>>> GetProductCampaignsAsync(string productId);


    }
}
