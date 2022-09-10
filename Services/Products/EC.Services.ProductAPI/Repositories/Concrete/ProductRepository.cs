using AutoMapper;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.ProductAPI.Constants;
using EC.Services.ProductAPI.Data.Abstract;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Entities;
using EC.Services.ProductAPI.Repositories.Abstract;
using MongoDB.Driver;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.ProductAPI.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(IProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Create
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
            var checkProduct = await (await _context.Products.FindAsync(x => x.Id == productAdded.Id)).AnyAsync();
            if(checkProduct)
            {
                return new ErrorResult(MessageExtensions.NotAdded(ProductEntities.Product));
            }
            return new SuccessResult(MessageExtensions.Added(ProductEntities.Product));
        }
        #endregion
        #region Update
        public async Task<IResult> UpdateAsync(ProductUpdateDto entity)
        {
            var productExists = await (await _context.Products.FindAsync(x => x.Id == entity.Id)).FirstOrDefaultAsync();
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
        #region Delete
        public async Task<IResult> DeleteAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);
            if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
            {
                return new SuccessResult(MessageExtensions.Deleted(ProductEntities.Product));
            }
            return new ErrorResult(MessageExtensions.NotDeleted(ProductEntities.Product));
        }
        #endregion
        #region Get
        public async Task<DataResult<ProductDto>> GetAsync(string id)
        {

            throw new NotImplementedException();
        }
        #endregion
        #region GetAll
        public async Task<DataResult<List<ProductDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetProductByCategoryId
        public async Task<List<ProductDto>> GetProductByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetProductByName
        public async Task<List<ProductDto>> GetProductByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
