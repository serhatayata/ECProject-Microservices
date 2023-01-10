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
        /// <summary>
        /// Gets data from DB by id
        /// </summary>
        /// <param name="id">id parameter of the campaign</param>
        /// <returns></returns>
        public async Task<DataResult<CampaignDto>> GetAsync(int id)
        {
            var campaign = await _campaignRepository.GetByIdAsync(id);
            if (campaign == null)
                return new ErrorDataResult<CampaignDto>(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            return new SuccessDataResult<CampaignDto>(_mapper.Map<CampaignDto>(campaign));
        }
        #endregion
        #region GetAllAsync
        /// <summary>
        /// Gets all campaign data from DB
        /// </summary>
        /// <returns></returns>
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
        #region GetWithStatusAsync
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Campaign Id</param>
        /// <param name="status">Campaign status</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DataResult<CampaignDto>> GetWithStatusByIdAsync(int id, CampaignStatus status)
        {
            var campaign = await _campaignRepository.GetWithStatusByIdAsync(id,status);
            if (campaign == null)
                return new ErrorDataResult<CampaignDto>(MessageExtensions.NotFound(DiscountConstantValues.Campaign));

            var result = _mapper.Map<CampaignDto>(campaign);
            return new SuccessDataResult<CampaignDto>(result);
        }
        #endregion
        #region GetAllWithStatusAsync
        /// <summary>
        /// Get all with status
        /// </summary>
        /// <param name="status">Campaign status</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DataResult<List<CampaignDto>>> GetAllWithStatusAsync(CampaignStatus status)
        {
            var campaigns = await _campaignRepository.GetAllWithStatusAsync(status);
            if (campaigns == null)
                return new ErrorDataResult<List<CampaignDto>>(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            else if (campaigns.Count() == 0)
                return new SuccessDataResult<List<CampaignDto>>(new List<CampaignDto>(), MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            else
                return new SuccessDataResult<List<CampaignDto>>(_mapper.Map<List<CampaignDto>>(campaigns));
        }
        #endregion
        #region GetProductCampaignsAsync
        /// <summary>
        /// Get campaigns from DB by product id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns></returns>
        public async Task<DataResult<List<CampaignDto>>> GetProductCampaignsAsync(string productId)
        {
            var campaigns = await _campaignRepository.GetAllByProductIdAsync(productId);
            if (campaigns == null)
                return new ErrorDataResult<List<CampaignDto>>(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            else if (campaigns.Count() == 0)
                return new SuccessDataResult<List<CampaignDto>>(new List<CampaignDto>(), MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            else
                return new SuccessDataResult<List<CampaignDto>>(_mapper.Map<List<CampaignDto>>(campaigns));
        }
        #endregion
        #region CreateAsync
        /// <summary>
        /// Creates a campaign
        /// </summary>
        /// <param name="entity">Campaign add dto</param>
        /// <returns></returns>
        public async Task<DataResult<CampaignDto>> CreateAsync(CampaignAddDto entity)
        {
            var campaignCodeExists = await _campaignRepository.GetWithStatusByCodeAsync(entity.CampaignCode);
            if (campaignCodeExists != null)
                return new ErrorDataResult<CampaignDto>(MessageExtensions.AlreadyExists(DiscountConstantValues.CampaignCode));
            var campaignAdded = _mapper.Map<Campaign>(entity);
            campaignAdded.CDate = DateTime.Now;
            await _campaignDal.AddAsync(campaignAdded);
            bool campaignAddResult = await _campaignRepository.GetByIdAsync(campaignAdded.Id) != null ? true : false;
            if (!campaignAddResult)
                return new ErrorDataResult<CampaignDto>(MessageExtensions.NotAdded(DiscountConstantValues.Campaign));
            return new SuccessDataResult<CampaignDto>(_mapper.Map<CampaignDto>(campaignAdded));
        }
        #endregion
        #region AddProductAsync
        /// <summary>
        /// Add product to an existing campaign
        /// </summary>
        /// <param name="model">Product dto for campaign</param>
        /// <returns></returns>
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
        /// <summary>
        /// Updates a campaign
        /// </summary>
        /// <param name="entity">Campaign update dto</param>
        /// <returns></returns>
        public async Task<DataResult<CampaignDto>> UpdateAsync(CampaignUpdateDto entity)
        {
            var campaignExists = await _campaignRepository.GetByIdAsync(entity.Id);
            if(campaignExists == null)
                return new ErrorDataResult<CampaignDto>(MessageExtensions.NotExists(DiscountConstantValues.Campaign));
            var campaignCodeExists = await _campaignDal.GetAsync(c => c.CampaignCode != campaignExists.CampaignCode && c.CampaignCode == entity.CampaignCode);
            if (campaignExists != null)
                return new ErrorDataResult<CampaignDto>(MessageExtensions.NotExists(DiscountConstantValues.Campaign));
            var campaignUpdated = _mapper.Map<CampaignUpdateDto, Campaign>(entity, campaignExists);
            campaignUpdated.UDate = DateTime.Now; 
            await _campaignDal.UpdateAsync(campaignUpdated);
            return new SuccessDataResult<CampaignDto>(_mapper.Map<CampaignDto>(campaignExists));
        }
        #endregion
        #region DeleteAsync
        /// <summary>
        /// Deletes campaign by id
        /// </summary>
        /// <param name="entity">Delete dto</param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(DeleteIntDto entity)
        {
            var campaign = await _campaignRepository.GetByIdAsync(entity.Id);
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
        /// <summary>
        /// Delete product from campaign
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Delete all products from a campaign
        /// </summary>
        /// <param name="model">Delete dto</param>
        /// <returns></returns>
        public async Task<IResult> DeleteAllProductsByCampaignIdAsync(DeleteIntDto model)
        {
            var campaignProductsDeleted = await _campaignProductRepository.DeleteAllProductsByCampaignIdAsync(model);
            if(campaignProductsDeleted.Success)
                return new SuccessResult(MessageExtensions.Deleted(DiscountConstantValues.Campaign));
            return new ErrorResult(MessageExtensions.NotDeleted(DiscountConstantValues.Campaign));
        }
        #endregion
        #region ActivateCampaignAsync
        /// <summary>
        /// Activate a campaign
        /// </summary>
        /// <param name="model">Int model</param>
        /// <returns></returns>
        public async Task<IResult> ActivateCampaignAsync(DeleteIntDto model)
        {
            var campaignExists = await _campaignDal.GetAsync(c => c.Id == model.Id);
            if(campaignExists == null)
                return new ErrorResult(MessageExtensions.NotExists(DiscountConstantValues.Campaign));
            if (campaignExists.Status == CampaignStatus.Deleted)
                return new ErrorResult(MessageExtensions.AlreadyDeleted(DiscountConstantValues.Campaign));
            campaignExists.Status = CampaignStatus.Deleted;
            await _campaignDal.UpdateAsync(campaignExists);
            var campaignDeleted = await _campaignRepository.GetWithStatusByIdAsync(model.Id,CampaignStatus.Deleted);
            if(campaignDeleted == null)
                return new ErrorResult(MessageExtensions.NotDeleted(DiscountConstantValues.Campaign));
            return new SuccessResult(MessageExtensions.Deleted(DiscountConstantValues.Campaign));
        }
        #endregion
    }
}
