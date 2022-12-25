using AutoMapper;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Data.Abstract.Dapper;
using EC.Services.DiscountAPI.Data.Abstract.EntityFramework;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignProduct;
using EC.Services.DiscountAPI.Entities;
using EC.Services.DiscountAPI.Services.Abstract;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Services.Concrete
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly ICampaignProductRepository _campaignProductRepository;
        private readonly ICampaignProductDal _campaignProductDal;
        private readonly ICampaignDal _campaignDal;
        private readonly IMapper _mapper;

        public CampaignService(ICampaignRepository campaignRepository, ICampaignProductRepository campaignProductRepository, ICampaignDal campaignDal, ICampaignProductDal campaignProductDal, IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _campaignDal = campaignDal;
            _campaignProductDal = campaignProductDal;
            _campaignProductRepository = campaignProductRepository;
            _mapper = mapper;
        }

        #region GetAsync
        public async Task<DataResult<CampaignDto>> GetAsync(int id)
        {
            var campaign = await _campaignRepository.GetByIdAsync(id);
            if (campaign == null)
                return new ErrorDataResult<CampaignDto>(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            return new SuccessDataResult<CampaignDto>(campaign);
        }
        #endregion
        #region GetAllAsync
        public async Task<DataResult<List<CampaignDto>>> GetAllAsync()
        {
            var campaigns = await _campaignRepository.GetAllAsync();
            if(campaigns == null)
                return new ErrorDataResult<List<CampaignDto>>(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            else if(campaigns.Count() == 0)
                return new SuccessDataResult<List<CampaignDto>>(new List<CampaignDto>(), MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            else
                return new SuccessDataResult<List<CampaignDto>>(_mapper.Map<List<CampaignDto>>(campaigns));
        }
        #endregion
        #region GetProductCampaignsAsync
        public async Task<DataResult<List<CampaignDto>>> GetProductCampaignsAsync(int productId)
        {
            var campaigns = await _campaignRepository.GetAllByProductIdAsync(productId);
            if (campaigns == null)
                return new ErrorDataResult<List<CampaignDto>>(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            else if (campaigns.Count() == 0)
                return new SuccessDataResult<List<CampaignDto>>(new List<CampaignDto>(), MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            else
                return new SuccessDataResult<List<CampaignDto>>(campaigns);
        }
        #endregion
        #region CreateAsync
        public async Task<DataResult<CampaignDto>> CreateAsync(CampaignAddDto entity)
        {
            var campaignAdded = _mapper.Map<Campaign>(entity);
            await _campaignDal.AddAsync(campaignAdded);
            bool campaignAddResult = await _campaignRepository.GetByIdAsync(campaignAdded.Id) != null ? true : false;
            if (!campaignAddResult)
                return new ErrorDataResult<CampaignDto>(MessageExtensions.NotAdded(DiscountConstantValues.Campaign));
            return new SuccessDataResult<CampaignDto>(_mapper.Map<CampaignDto>(campaignAdded));
        }
        #endregion
        #region AddProductAsync
        public async Task<DataResult<CampaignProductDto>> AddProductAsync(CampaignAddProductDto model)
        {
            var campaignProduct = _mapper.Map<CampaignProduct>(model);
            var cpExists = _campaignProductDal.GetAsync(cp => cp.CampaignId == model.CampaignId && cp.ProductId == model.ProductId);
            if (cpExists == null)
                return new ErrorDataResult<CampaignProductDto>(MessageExtensions.AlreadyExists(DiscountConstantValues.CampaignProduct));
            await _campaignProductDal.AddAsync(campaignProduct);
            bool campaignProductAdded = _campaignProductDal.GetAsync(cp => cp.CampaignId == model.CampaignId && cp.ProductId == model.ProductId) != null ? true : false;
            if(!campaignProductAdded)
                return new ErrorDataResult<CampaignProductDto>(MessageExtensions.NotAdded(DiscountConstantValues.Campaign));
            return new SuccessDataResult<CampaignProductDto>(_mapper.Map<CampaignProductDto>(campaignProduct));
        }
        #endregion
        #region UpdateAsync
        public async Task<DataResult<CampaignDto>> UpdateAsync(CampaignUpdateDto entity)
        {
            var campaignExists = await _campaignDal.GetAsync(c => c.Id == entity.Id);
            if(campaignExists == null)
                return new ErrorDataResult<CampaignDto>(MessageExtensions.NotExists(DiscountConstantValues.Campaign));
            var campaignUpdated = _mapper.Map<CampaignUpdateDto, Campaign>(entity, campaignExists);
            campaignUpdated.UDate = DateTime.Now; 
            await _campaignDal.UpdateAsync(campaignUpdated);
            return new SuccessDataResult<CampaignDto>(_mapper.Map<CampaignDto>(campaignExists));
        }
        #endregion
        #region DeleteAsync
        public async Task<IResult> DeleteAsync(DeleteIntDto entity)
        {
            var campaign = await _campaignDal.GetAsync(c => c.Id == entity.Id && c.Status == CampaignStatus.Active);
            if (campaign == null)
                return new ErrorResult(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            campaign.Status = CampaignStatus.Deleted;
            await _campaignDal.UpdateAsync(campaign);
            var campaignDeleted = await _campaignDal.GetAsync(c => c.Id == campaign.Id);
            if (campaignDeleted.Status == CampaignStatus.Deleted)
                return new SuccessResult(MessageExtensions.Deleted(DiscountConstantValues.Campaign));
            return new ErrorResult(MessageExtensions.NotDeleted(DiscountConstantValues.Campaign));
        }
        #endregion
        #region DeleteProductAsync
        public async Task<IResult> DeleteProductAsync(CampaignDeleteProductDto model)
        {
            var campaignProduct = await _campaignProductDal.GetAsync(cp => cp.CampaignId == model.CampaignId && cp.ProductId == model.ProductId);
            if (campaignProduct == null)
                return new ErrorResult(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            await _campaignProductDal.DeleteAsync(campaignProduct);
            var campaignProductDeleted = await _campaignProductDal.GetAsync(cp => cp.CampaignId == model.CampaignId && cp.ProductId == model.ProductId);
            if (campaignProductDeleted == null)
                return new SuccessResult(MessageExtensions.Deleted(DiscountConstantValues.Campaign));
            return new ErrorResult(MessageExtensions.NotDeleted(DiscountConstantValues.Campaign));
        }
        #endregion
        #region DeleteAllProductsByCampaignIdAsync
        public async Task<IResult> DeleteAllProductsByCampaignIdAsync(DeleteIntDto model)
        {
            var campaignProductsDeleted = await _campaignProductRepository.DeleteAllProductsByCampaignIdAsync(model);
            if(campaignProductsDeleted.Success)
                return new SuccessResult(MessageExtensions.Deleted(DiscountConstantValues.Campaign));
            return new ErrorResult(MessageExtensions.NotDeleted(DiscountConstantValues.Campaign));
        }
        #endregion
    }
}
