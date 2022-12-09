using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.ProductAPI.Constants;
using EC.Services.ProductAPI.Data.Abstract;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Dtos.ProductVariantDtos;
using EC.Services.ProductAPI.Dtos.VariantDtos;
using EC.Services.ProductAPI.Entities;
using EC.Services.ProductAPI.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using Nest;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.ProductAPI.Repositories.Concrete
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly IProductContext _context;
        private readonly IMapper _mapper;

        public ProductVariantRepository(IProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CreateAsync
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        [CacheRemoveAspect("IProductVariantRepository")]
        public async Task<IResult> CreateAsync(ProductVariantDto entity)
        {
            #region Product and Variant check
            var productExistsGet = await _context.Products.FindAsync(x => x.Id == entity.ProductId);
            var productExists = await productExistsGet.FirstOrDefaultAsync();
            if (productExists == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(ProductEntities.Product));
            }
            var variantExistsGet = await _context.Variants.FindAsync(x => x.Id == entity.VariantId);
            var variantExists = await variantExistsGet.FirstOrDefaultAsync();
            if (variantExists == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(ProductEntities.Variant));
            }
            #endregion
                
            var prodVarExistsGet = await _context.ProductVariants.FindAsync(x => x.Id == entity.ProductId && x.VariantId==entity.VariantId);
            var prodVarExists = await prodVarExistsGet.FirstOrDefaultAsync();
            if (prodVarExists != null)
            {
                return new ErrorResult(MessageExtensions.AlreadyExists(ProductEntities.ProductVariant));
            }

            var prodVarAdded = _mapper.Map<ProductVariant>(entity);
            await _context.ProductVariants.InsertOneAsync(prodVarAdded);
            var checkProdVar = await _context.ProductVariants.FindAsync(x => x.ProductId == prodVarAdded.ProductId && x.VariantId == prodVarAdded.VariantId);
            if (checkProdVar == null)
            {
                return new ErrorResult(MessageExtensions.NotAdded(ProductEntities.ProductVariant));
            }
            return new SuccessResult(MessageExtensions.Added(ProductEntities.ProductVariant));
        }
        #endregion
        #region UpdateAsync
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        [CacheRemoveAspect("IProductVariantRepository")]
        public async Task<IResult> UpdateAsync(ProductVariantDto entity)
        {
            var prodVarExistsGet = await _context.ProductVariants.FindAsync(x => x.ProductId == entity.ProductId && x.VariantId == entity.VariantId);
            var prodVarExists = await prodVarExistsGet.FirstOrDefaultAsync();
            if (prodVarExists == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(ProductEntities.ProductVariant));
            }

            var prodVarUpdated = _mapper.Map<ProductVariantDto, ProductVariant>(entity, prodVarExists);
            var updateResult = await _context.ProductVariants.ReplaceOneAsync(x => x.ProductId == prodVarUpdated.ProductId && x.VariantId == prodVarUpdated.VariantId,prodVarUpdated);
            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Updated(ProductEntities.ProductVariant));
            }
            return new ErrorResult(MessageExtensions.NotUpdated(ProductEntities.ProductVariant));
        }
        #endregion
        #region DeleteAsync
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        [CacheRemoveAspect("IProductVariantRepository")]
        public async Task<IResult> DeleteAsync(string productId)
        {
            var filter = Builders<ProductVariant>.Filter.Eq(m => m.ProductId, productId);
            DeleteResult deleteResult = await _context.ProductVariants.DeleteManyAsync(filter);
            if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Deleted(ProductEntities.ProductVariant));
            }
            return new ErrorResult(MessageExtensions.NotDeleted(ProductEntities.ProductVariant));
        }
        #endregion
        #region DeleteByProductAndVariantId
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        [CacheRemoveAspect("IProductVariantRepository")]
        public async Task<IResult> DeleteByProductAndVariantId(string productId, string variantId)
        {
            var filter = Builders<ProductVariant>.Filter.Eq(m => m.ProductId, productId) & Builders<ProductVariant>.Filter.Eq(x=>x.VariantId,variantId);
            DeleteResult deleteResult = await _context.ProductVariants.DeleteOneAsync(filter);
            if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Deleted(ProductEntities.ProductVariant));
            }
            return new ErrorResult(MessageExtensions.NotDeleted(ProductEntities.ProductVariant));
        }
        #endregion
        #region GetAsync
        public async Task<DataResult<ProductVariantDto>> GetAsync(string id)
        {
            var query = await _context.ProductVariants.FindAsync(p => p.ProductId == id);
            var result = await query.FirstOrDefaultAsync();
            if (result != null)
            {
                var product = _mapper.Map<ProductVariantDto>(result);
                return new SuccessDataResult<ProductVariantDto>(product);
            }
            return new ErrorDataResult<ProductVariantDto>(MessageExtensions.NotFound(ProductEntities.ProductVariant));
        }
        #endregion
        #region GetByProductAndVariantId
        public async Task<DataResult<ProductVariantDto>> GetByProductAndVariantId(string productId, string variantId)
        {
            var query = await _context.ProductVariants.FindAsync(p => p.ProductId == productId && p.VariantId == variantId);
            var result = await query.FirstOrDefaultAsync();
            if (result != null)
            {
                var product = _mapper.Map<ProductVariantDto>(result);
                return new SuccessDataResult<ProductVariantDto>(product);
            }
            return new ErrorDataResult<ProductVariantDto>(MessageExtensions.NotFound(ProductEntities.ProductVariant));
        }
        #endregion
        #region GetAllAsync
        [CacheAspect(duration: 60)]
        public async Task<DataResult<List<ProductVariantDto>>> GetAllAsync()
        {
            var query = await _context.ProductVariants.FindAsync(p => true);
            var result = await query.ToListAsync();
            if (result != null)
            {
                var product = _mapper.Map<List<ProductVariantDto>>(result);
                return new SuccessDataResult<List<ProductVariantDto>>(product);
            }
            return new ErrorDataResult<List<ProductVariantDto>>(MessageExtensions.NotFound(ProductEntities.ProductVariant));
        }
        #endregion

    }
}
