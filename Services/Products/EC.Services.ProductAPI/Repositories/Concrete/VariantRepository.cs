using AutoMapper;
using Core.Aspects.Autofac.Caching;
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
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.ProductAPI.Repositories.Concrete
{
    public class VariantRepository : IVariantRepository
    {
        private readonly IProductContext _context;
        private readonly IMapper _mapper;

        public VariantRepository(IProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CreateAsync
        [RedisCacheRemoveAspect("IVariantRepository", Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> CreateAsync(VariantAddDto entity)
        {
            var variantExistsGet = await _context.Variants.FindAsync(x => x.Name == entity.Name);
            var variantExists = await variantExistsGet.FirstOrDefaultAsync();
            if (variantExists != null)
            {
                return new ErrorResult(MessageExtensions.AlreadyExists(ProductEntities.Variant));
            }

            var variantAdded = _mapper.Map<Variant>(entity);
            await _context.Variants.InsertOneAsync(variantAdded);
            var checkVariant = await _context.Variants.FindAsync(x => x.Id == variantAdded.Id);
            if (checkVariant == null)
            {
                return new ErrorResult(MessageExtensions.NotAdded(ProductEntities.Variant));
            }
            return new SuccessResult(MessageExtensions.Added(ProductEntities.Variant));
        }
        #endregion
        #region UpdateAsync
        [RedisCacheRemoveAspect("IVariantRepository", Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> UpdateAsync(VariantUpdateDto entity)
        {
            var variantExistsGet = await _context.Variants.FindAsync(x => x.Id == entity.Id);
            var variantExists = await variantExistsGet.FirstOrDefaultAsync();
            if (variantExists == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(ProductEntities.Variant));
            }

            var variantNameExistsGet = await _context.Variants.FindAsync(x => x.Name != variantExists.Name && x.Name == entity.Name);
            var variantNameExists = await variantNameExistsGet.FirstOrDefaultAsync();
            if (variantNameExists != null)
            {
                return new ErrorResult(MessageExtensions.AlreadyExists(ProductEntities.Variant));
            }

            var variantUpdated = _mapper.Map<VariantUpdateDto, Variant>(entity, variantExists);

            var updateResult = await _context.Variants.ReplaceOneAsync(x=>x.Id == variantUpdated.Id, variantUpdated);
            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Updated(ProductEntities.Variant));
            }
            return new ErrorResult(MessageExtensions.NotUpdated(ProductEntities.Variant));
        }
        #endregion
        #region DeleteAsync
        [RedisCacheRemoveAspect("IVariantRepository", Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> DeleteAsync(string id)
        {
            var filter = Builders<Variant>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context.Variants.DeleteOneAsync(filter);
            if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Deleted(ProductEntities.Variant));
            }
            return new ErrorResult(MessageExtensions.NotDeleted(ProductEntities.Variant));
        }
        #endregion
        #region GetAsync
        public async Task<DataResult<VariantDto>> GetAsync(string id)
        {
            var query = await _context.Variants.FindAsync(p => p.Id == id);
            var result = await query.FirstOrDefaultAsync();
            if (result != null)
            {
                var product = _mapper.Map<VariantDto>(result);
                return new SuccessDataResult<VariantDto>(product);
            }
            return new ErrorDataResult<VariantDto>(MessageExtensions.NotFound(ProductEntities.Variant));
        }
        #endregion
        #region GetAllAsync
        [RedisCacheAspect<DataResult<List<VariantDto>>>(duration: 60)]
        public async Task<DataResult<List<VariantDto>>> GetAllAsync()
        {
            var query = await _context.Variants.FindAsync(p => true);
            var result = await query.ToListAsync();
            if (result != null)
            {
                var product = _mapper.Map<List<VariantDto>>(result);
                return new SuccessDataResult<List<VariantDto>>(product);
            }
            return new ErrorDataResult<List<VariantDto>>(MessageExtensions.NotFound(ProductEntities.Variant));
        }
        #endregion
        #region GetAllPagingAsync
        [RedisCacheAspect<DataResult<List<VariantDto>>>(duration: 60)]
        public async Task<DataResult<List<VariantDto>>> GetAllPagingAsync(int page = 1, int pageSize = 8)
        {
            var result = _context.VariantsAsQueryable.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            if (result != null)
            {
                var product = _mapper.Map<List<VariantDto>>(result);
                return new SuccessDataResult<List<VariantDto>>(product);
            }
            return new ErrorDataResult<List<VariantDto>>(MessageExtensions.NotFound(ProductEntities.Variant));
        }
        #endregion

    }
}
