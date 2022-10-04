using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.DiscountAPI.Constants;
using EC.Services.DiscountAPI.Data.Abstract;
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
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDiscountContext _context;
        private readonly IMapper _mapper;

        public DiscountRepository(IDiscountContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CreateAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> CreateAsync(DiscountAddDto entity)
        {
            var discountAdded = _mapper.Map<Discount>(entity);

            string code = RandomExtensions.RandomString(10);

            discountAdded.CDate = DateTime.Now;
            discountAdded.Status = true;
            discountAdded.Code = code;

            await _context.Discounts.InsertOneAsync(discountAdded);
            var checkDiscount = await _context.DiscountsAsQueryable.AnyAsync(x => x.Id == discountAdded.Id);
            if (checkDiscount)
            {
                return new ErrorResult(MessageExtensions.NotAdded(DiscountConstantValues.Discount));
            }
            return new SuccessResult(MessageExtensions.Added(DiscountConstantValues.Discount));
        }
        #endregion
        #region UpdateAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> UpdateAsync(DiscountUpdateDto entity)
        {
            var discountExists = await _context.DiscountsAsQueryable.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (discountExists == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(DiscountConstantValues.Discount));
            }

            var discountUpdated = _mapper.Map<DiscountUpdateDto, Discount>(entity, discountExists);

            var updateResult = await _context.Discounts.ReplaceOneAsync(g => g.Id == entity.Id, discountUpdated);
            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Updated(DiscountConstantValues.Discount));
            }
            return new ErrorResult(MessageExtensions.NotUpdated(DiscountConstantValues.Discount));
        }
        #endregion
        #region DeleteAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> DeleteAsync(string id)
        {
            var entity = _context.DiscountsAsQueryable.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(DiscountConstantValues.Discount));
            }
            var filter = Builders<Discount>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context.Discounts.DeleteOneAsync(filter);
            if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Deleted(DiscountConstantValues.Discount));
            }
            return new ErrorResult(MessageExtensions.NotDeleted(DiscountConstantValues.Discount));
        }
        #endregion
        #region GetAllAsync
        public async Task<DataResult<List<DiscountDto>>> GetAllAsync()
        {
            var query = await _context.Discounts.FindAsync(p => p.Status);
            var result = await query.ToListAsync();
            if (result != null)
            {
                var product = _mapper.Map<List<DiscountDto>>(result);
                return new SuccessDataResult<List<DiscountDto>>(product);
            }
            return new ErrorDataResult<List<DiscountDto>>(MessageExtensions.NotFound(DiscountConstantValues.Discount));
        }
        #endregion
        #region GetAsync
        public async Task<DataResult<DiscountDto>> GetAsync(string id)
        {
            var query = await _context.Discounts.FindAsync(p => p.Id == id && p.Status);
            var result = await query.FirstOrDefaultAsync();
            if (result != null)
            {
                var discount = _mapper.Map<DiscountDto>(result);
                return new SuccessDataResult<DiscountDto>(discount);
            }
            return new ErrorDataResult<DiscountDto>(MessageExtensions.NotFound(DiscountConstantValues.Discount));
        }
        #endregion
        #region GetDiscountByCodeAsync
        public async Task<DataResult<DiscountDto>> GetDiscountByCodeAsync(string code)
        {
            var query = await _context.Discounts.FindAsync(p => p.Code == code && p.Status);
            var result = await query.FirstOrDefaultAsync();
            if (result != null)
            {
                var product = _mapper.Map<DiscountDto>(result);
                return new SuccessDataResult<DiscountDto>(product);
            }
            return new ErrorDataResult<DiscountDto>(MessageExtensions.NotFound(DiscountConstantValues.Discount));
        }
        #endregion


    }
}
