using Core.Utilities.Results;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Entities;

namespace EC.Services.ProductAPI.Repositories.Abstract
{
    public interface IProductRepository:IBaseRepository<ProductDto,ProductAddDto,ProductUpdateDto>
    {
        Task<DataResult<List<ProductDto>>> GetProductsByNameAsync(string name);
        Task<DataResult<List<ProductDto>>> GetProductsByCategoryIdAsync(int categoryId);
    }
}
