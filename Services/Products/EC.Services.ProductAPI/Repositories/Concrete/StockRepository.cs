using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.ProductAPI.Constants;
using EC.Services.ProductAPI.Data.Abstract;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Dtos.StockDtos;
using EC.Services.ProductAPI.Dtos.VariantDtos;
using EC.Services.ProductAPI.Entities;
using EC.Services.ProductAPI.Repositories.Abstract;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using Nest;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.ProductAPI.Repositories.Concrete
{
    public class StockRepository : IStockRepository
    {
        private readonly IProductContext _context;
        private readonly IMapper _mapper;

        public StockRepository(IProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CreateAsync
        [CacheRemoveAspect("IStockRepository")]
        public async Task<IResult> CreateAsync(StockAddDto entity)
        {
            var stockExistsGet = await _context.Stocks.FindAsync(x => x.ProductId == entity.ProductId);
            var stockExists = await stockExistsGet.FirstOrDefaultAsync();
            if (stockExists != null)
            {
                return new ErrorResult(MessageExtensions.AlreadyExists(ProductEntities.Stock));
            }

            var stockAdded = _mapper.Map<Stock>(entity);
            await _context.Stocks.InsertOneAsync(stockAdded);
            var checkStock = await _context.Stocks.FindAsync(x => x.Id == stockAdded.Id);
            if (checkStock == null)
            {
                return new ErrorResult(MessageExtensions.NotAdded(ProductEntities.Stock));
            }
            return new SuccessResult(MessageExtensions.Added(ProductEntities.Stock));
        }
        #endregion
        #region UpdateAsync
        [CacheRemoveAspect("IStockRepository")]
        public async Task<IResult> UpdateAsync(StockUpdateDto entity)
        {
            var stockExistsGet = await _context.Stocks.FindAsync(x => x.Id == entity.Id && x.ProductId==entity.ProductId);
            var stockExists = await stockExistsGet.FirstOrDefaultAsync();
            if (stockExists == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(ProductEntities.Stock));
            }

            var stockUpdated = _mapper.Map<StockUpdateDto, Stock>(entity, stockExists);

            var updateResult = await _context.Stocks.ReplaceOneAsync(x => x.Id == stockUpdated.Id && x.ProductId==stockUpdated.ProductId, stockUpdated);
            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Updated(ProductEntities.Stock));
            }
            return new ErrorResult(MessageExtensions.NotUpdated(ProductEntities.Stock));
        }
        #endregion
        #region DeleteAsync
        [CacheRemoveAspect("IStockRepository")]
        public async Task<IResult> DeleteAsync(string id)
        {
            var filter = Builders<Stock>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context.Stocks.DeleteOneAsync(filter);
            if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Deleted(ProductEntities.Stock));
            }
            return new ErrorResult(MessageExtensions.NotDeleted(ProductEntities.Stock));
        }
        #endregion
        #region GetAllAsync
        [CacheAspect(duration: 60)]
        public async Task<DataResult<List<StockDto>>> GetAllAsync()
        {
            var query = await _context.Stocks.FindAsync(p => true);
            var result = await query.ToListAsync();
            if (result != null)
            {
                var product = _mapper.Map<List<StockDto>>(result);
                return new SuccessDataResult<List<StockDto>>(product);
            }
            return new ErrorDataResult<List<StockDto>>(MessageExtensions.NotFound(ProductEntities.Stock));
        }
        #endregion
        #region GetAllPagingAsync
        [CacheAspect(duration: 60)]
        public async Task<DataResult<List<StockDto>>> GetAllPagingAsync(int page = 1, int pageSize = 8)
        {
            var result = _context.StocksAsQueryable.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            if (result != null)
            {
                var stock = _mapper.Map<List<StockDto>>(result);
                return new SuccessDataResult<List<StockDto>>(stock);
            }
            return new ErrorDataResult<List<StockDto>>(MessageExtensions.NotFound(ProductEntities.Stock));
        }
        #endregion
        #region GetAsync
        public async Task<DataResult<StockDto>> GetAsync(string id)
        {
            var query = await _context.Stocks.FindAsync(p => p.Id == id);
            var result = await query.FirstOrDefaultAsync();
            if (result != null)
            {
                var stock = _mapper.Map<StockDto>(result);
                return new SuccessDataResult<StockDto>(stock);
            }
            return new ErrorDataResult<StockDto>(MessageExtensions.NotFound(ProductEntities.Stock));
        }
        #endregion
        #region GetByProductIdAsync
        public async Task<DataResult<StockDto>> GetByProductIdAsync(string productId)
        {
            var query = await _context.Stocks.FindAsync(p => p.ProductId == productId);
            var result = await query.FirstOrDefaultAsync();
            if (result != null)
            {
                var stock = _mapper.Map<StockDto>(result);
                return new SuccessDataResult<StockDto>(stock);
            }
            return new ErrorDataResult<StockDto>(MessageExtensions.NotFound(ProductEntities.Stock));
        }
        #endregion



    }
}
