using Core.Utilities.Results;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Dtos.ProductVariantDtos;
using EC.Services.ProductAPI.Entities;

namespace EC.Services.ProductAPI.Repositories.Abstract
{
    public interface IProductRepository:IBaseRepository<ProductDto,ProductAddDto,ProductUpdateDto>
    {
        Task<DataResult<List<ProductDto>>> GetProductsByNameAsync(string name);
        Task<DataResult<List<ProductDto>>> GetProductsByCategoryIdAsync(int categoryId);
        Task<DataResult<List<ProductDto>>> GetAllPagingAsync(int page = 1, int pageSize = 8);
        Task<DataResult<List<ProductDto>>> GetProductsByProductIds(ProductGetProductsByIdsDto model);


    }
}
