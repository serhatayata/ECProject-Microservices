using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.DataAccess.Queue;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.ProductAPI.Constants;
using EC.Services.ProductAPI.Data.Abstract;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Entities;
using EC.Services.ProductAPI.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Nest;
using System.Collections.Generic;
using IResult = Core.Utilities.Results.IResult;
using Mass = MassTransit;

namespace EC.Services.ProductAPI.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _context;
        private readonly IMapper _mapper;
        private readonly Mass.IPublishEndpoint _publishEndpoint;

        public ProductRepository(IProductContext context, IMapper mapper, Mass.IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        #region CreateAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        [CacheRemoveAspect("IProductRepository")]
        public async Task<IResult> CreateAsync(ProductAddDto entity)
        {
            #region Link
            string linkSlug = SeoLinkExtensions.GenerateSlug(entity.Name);
            string guidKey = Guid.NewGuid().ToString();
            string link = linkSlug + "_" + guidKey;
            #endregion
            #region LastLine
            int lastLine = _context.Products.Find(x => true)?.SortByDescending(x => x.Line)?.First()?.Line ?? 1;
            #endregion

            var productAdded = _mapper.Map<Product>(entity);
            productAdded.Link = link;
            productAdded.Line = lastLine + 1;
            productAdded.CreatedAt = DateTime.Now;
            productAdded.Status = true;

            await _context.Products.InsertOneAsync(productAdded);
            var checkProduct = await _context.ProductsAsQueryable.AnyAsync(x => x.Id == productAdded.Id);
            //var checkProduct = await (await _context.Products.FindAsync(x => x.Id == productAdded.Id)).AnyAsync();
            if(checkProduct)
            {
                return new ErrorResult(MessageExtensions.NotAdded(ProductEntities.Product));
            }
            return new SuccessResult(MessageExtensions.Added(ProductEntities.Product));
        }
        #endregion
        #region UpdateAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        [CacheRemoveAspect("IProductRepository")]
        public async Task<IResult> UpdateAsync(ProductUpdateDto entity)
        {
            //var productExists = await (await _context.Products.FindAsync(x => x.Id == entity.Id)).FirstOrDefaultAsync();
            var productExists = await _context.ProductsAsQueryable.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (productExists == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(ProductEntities.Product));
            }

            #region Link
            if (entity.Name != productExists.Name)
            {
                string linkSlug = SeoLinkExtensions.GenerateSlug(entity.Name);
                string guidKey = Guid.NewGuid().ToString();
                string link = linkSlug + "_" + guidKey;

                productExists.Link = link;
            }
            #endregion

            var productUpdated = _mapper.Map<ProductUpdateDto, Product>(entity, productExists);

            var updateResult = await _context.Products.ReplaceOneAsync(g => g.Id == entity.Id, productUpdated);
            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Updated(ProductEntities.Product));
            }
            return new ErrorResult(MessageExtensions.NotUpdated(ProductEntities.Product));
        }
        #endregion
        #region DeleteAsync
        [ElasticSearchLogAspect(risk: 1, Priority = 1)]
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        [CacheRemoveAspect("IProductRepository")]
        public async Task<IResult> DeleteAsync(string id)
        {
            var entity = _context.ProductsAsQueryable.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(ProductEntities.Product));
            }
            var filter = Builders<Product>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);
            if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
            {
                await _publishEndpoint.Publish<ProductDeletedEvent>(new ProductDeletedEvent { ProductId=id });
                return new SuccessResult(MessageExtensions.Deleted(ProductEntities.Product));
            }
            return new ErrorResult(MessageExtensions.NotDeleted(ProductEntities.Product));
        }
        #endregion
        #region GetAsync
        public async Task<DataResult<ProductDto>> GetAsync(string id)
        {
            var query = await _context.Products.FindAsync(p => p.Id == id && p.Status);
            var result = await query.FirstOrDefaultAsync();
            if (result != null)
            {
                var product = _mapper.Map<ProductDto>(result);
                return new SuccessDataResult<ProductDto>(product);
            }
            return new ErrorDataResult<ProductDto>(MessageExtensions.NotFound(ProductEntities.Product));
        }
        #endregion
        #region GetAllAsync
        [CacheAspect(duration: 60)]
        public async Task<DataResult<List<ProductDto>>> GetAllAsync()
        {
            var query = await _context.Products.FindAsync(p => p.Status);
            var result = await query.ToListAsync();
            if (result != null)
            {
                var product = _mapper.Map<List<ProductDto>>(result);
                return new SuccessDataResult<List<ProductDto>>(product);
            }
            return new ErrorDataResult<List<ProductDto>>(MessageExtensions.NotFound(ProductEntities.Product));
        }
        #endregion
        #region GetAllPagingAsync
        [CacheAspect(duration: 60)]
        public async Task<DataResult<List<ProductDto>>> GetAllPagingAsync(int page=1,int pageSize=8)
        {
            throw new Exception("test deneme error");
            var result = _context.ProductsAsQueryable.Where(x => x.Status).OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            if (result != null)
            {
                var product = _mapper.Map<List<ProductDto>>(result);
                return new SuccessDataResult<List<ProductDto>>(product);
            }
            return new ErrorDataResult<List<ProductDto>>(MessageExtensions.NotFound(ProductEntities.Product));
        }
        #endregion
        #region GetProductByCategoryIdAsync
        [CacheAspect(duration: 60)]
        public async Task<DataResult<List<ProductDto>>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var result = await _context.Products.Find(p => p.CategoryId == categoryId && p.Status).ToListAsync();
            if (result != null)
            {
                var product = _mapper.Map<List<ProductDto>>(result);
                return new SuccessDataResult<List<ProductDto>>(product);
            }
            return new ErrorDataResult<List<ProductDto>>(MessageExtensions.NotFound(ProductEntities.Product));
        }
        #endregion
        #region GetProductByNameAsync
        [CacheAspect(duration: 60)]
        public async Task<DataResult<List<ProductDto>>> GetProductsByNameAsync(string name)
        {
            var result = await _context.Products.Find(p => p.Name.Contains(name) && p.Status).ToListAsync();
            if (result != null)
            {
                var product = _mapper.Map<List<ProductDto>>(result);
                return new SuccessDataResult<List<ProductDto>>(product);
            }
            return new ErrorDataResult<List<ProductDto>>(MessageExtensions.NotFound(ProductEntities.Product));
        }
        #endregion
        #region GetProductsByProductIds
        public async Task<DataResult<List<ProductDto>>> GetProductsByProductIds(ProductGetProductsByIdsDto model)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.In(x => x.Id, model.Ids);
            var filterStatus = builder.Eq(x => x.Status,true);

            var resultGet = await _context.Products.FindAsync(filter & filterStatus);
            if (resultGet != null)
            {
                var result = resultGet.ToList();
                var product = _mapper.Map<List<ProductDto>>(result);
                return new SuccessDataResult<List<ProductDto>>(product);
            }
            return new ErrorDataResult<List<ProductDto>>(MessageExtensions.NotFound(ProductEntities.Product));
        }
        #endregion


    }
}
