using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Services.Abstract;

namespace EC.Services.DiscountAPI.Services.Concrete
{
    public class CampaignService : ICampaignService
    {
        public Task<Core.Utilities.Results.IResult> AddProductsAsync(CampaignAddProductsDto model)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<CampaignDto>> CreateAsync(CampaignAddDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Utilities.Results.IResult> DeleteAllProductsByCampaignIdAsync(DeleteIntDto model)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Utilities.Results.IResult> DeleteAsync(DeleteIntDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Utilities.Results.IResult> DeleteProductAsync(CampaignDeleteProductDto model)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<List<CampaignDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<CampaignDto>> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<List<CampaignDto>>> GetProductCampaignsAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<CampaignDto>> UpdateAsync(CampaignUpdateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
