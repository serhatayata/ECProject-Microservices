using AutoMapper;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Data.Abstract;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.Discount;
using EC.Services.DiscountAPI.Entities;
using EC.Services.DiscountAPI.Extensions;
using EC.Services.DiscountAPI.Repositories.Abstract;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Nest;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Repositories.Concrete
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly IDiscountContext _context;
        private readonly IMapper _mapper;

        public CampaignRepository(IDiscountContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CreateAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> CreateAsync(CampaignAddDto entity)
        {
            var campaignAdded = _mapper.Map<Campaign>(entity);

            campaignAdded.CDate = DateTime.Now;
            campaignAdded.Status = true;

            await _context.Campaigns.InsertOneAsync(campaignAdded);
            var checkCampaign = await _context.CampaignsAsQueryable.AnyAsync(x => x.Id == campaignAdded.Id);
            if (checkCampaign)
            {
                return new ErrorResult(MessageExtensions.NotAdded(DiscountConstantValues.Campaign));
            }
            return new SuccessResult(MessageExtensions.Added(DiscountConstantValues.Campaign));
        }
        #endregion
        #region UpdateAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> UpdateAsync(CampaignUpdateDto entity)
        {
            var campaignExists = await _context.CampaignsAsQueryable.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (campaignExists == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            }

            var campaignUpdated = _mapper.Map<CampaignUpdateDto, Campaign>(entity, campaignExists);

            var updateResult = await _context.Campaigns.ReplaceOneAsync(g => g.Id == entity.Id, campaignUpdated);
            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Updated(DiscountConstantValues.Campaign));
            }
            return new ErrorResult(MessageExtensions.NotUpdated(DiscountConstantValues.Campaign));
        }
        #endregion
        #region DeleteAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> DeleteAsync(string id)
        {
            var entity = _context.CampaignsAsQueryable.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            }
            var filter = Builders<Campaign>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context.Campaigns.DeleteOneAsync(filter);
            if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Deleted(DiscountConstantValues.Campaign));
            }
            return new ErrorResult(MessageExtensions.NotDeleted(DiscountConstantValues.Campaign));
        }
        #endregion
        #region DeleteProductAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> DeleteProductAsync(CampaignDeleteProductDto model)
        {
            var entity = _context.CampaignsAsQueryable.FirstOrDefault(x => x.Id == model.CampaignId);
            if (entity == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
            }

            var products = entity.Products;
            var deletedProducts = products.RemoveAll(x => x == model.ProductId);
            if (deletedProducts <= 0)
            {
                return new ErrorResult(MessageExtensions.NotDeleted(DiscountConstantValues.Campaign));
            }

            entity.Products = products;

            var updateResult = await _context.Campaigns.ReplaceOneAsync(g => g.Id == entity.Id, entity);
            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Deleted(DiscountConstantValues.Campaign));
            }
            return new ErrorResult(MessageExtensions.NotDeleted(DiscountConstantValues.Campaign));
        }
        #endregion
        #region AddProductsAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> AddProductsAsync(CampaignAddProductsDto model)
        {
            var query = await _context.Campaigns.FindAsync(p => p.Id == model.CampaignId && p.Status);
            var result = await query.FirstOrDefaultAsync();
            if (result != null)
            {
                result.Products.AddRange(model.ProductIds);
                var updateResult = await _context.Campaigns.ReplaceOneAsync(g => g.Id == result.Id, result);
                if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
                {
                    var updatedCampaign = _mapper.Map<CampaignDto>(result);
                    return new SuccessDataResult<CampaignDto>(MessageExtensions.OneAddedToOne(DiscountConstantValues.Product,DiscountConstantValues.Campaign));
                }
                return new ErrorDataResult<CampaignDto>(MessageExtensions.OneNotAddedToOne(DiscountConstantValues.Product, DiscountConstantValues.Campaign));
            }
            return new ErrorDataResult<CampaignDto>(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
        }
        #endregion
        #region GetProductCampaignsAsync
        public async Task<DataResult<List<CampaignDto>>> GetProductCampaignsAsync(string productId)
        {
            var query = await _context.Campaigns.FindAsync(p => p.Products.Contains(productId) && p.Status);
            var result = await query.ToListAsync();
            if (result != null)
            {
                var campaigns = _mapper.Map<List<CampaignDto>>(result);
                return new SuccessDataResult<List<CampaignDto>>(campaigns);
            }
            return new ErrorDataResult<List<CampaignDto>>(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
        }
        #endregion
        #region GetAllAsync
        public async Task<DataResult<List<CampaignDto>>> GetAllAsync()
        {
            var query = await _context.Campaigns.FindAsync(p=> p.Status);
            var result = await query.ToListAsync();
            if (result != null)
            {
                var campaigns = _mapper.Map<List<CampaignDto>>(result);
                return new SuccessDataResult<List<CampaignDto>>(campaigns);
            }
            return new ErrorDataResult<List<CampaignDto>>(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
        }
        #endregion
        #region GetAsync
        public async Task<DataResult<CampaignDto>> GetAsync(string id)
        {
            var query = await _context.Campaigns.FindAsync(p => p.Id == id && p.Status);
            var result = await query.FirstOrDefaultAsync();
            if (result != null)
            {
                var campaign = _mapper.Map<CampaignDto>(result);
                return new SuccessDataResult<CampaignDto>(campaign);
            }
            return new ErrorDataResult<CampaignDto>(MessageExtensions.NotFound(DiscountConstantValues.Campaign));
        }
        #endregion


    }
}
