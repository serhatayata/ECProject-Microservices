using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Entities;

namespace EC.Services.ProductAPI.Repositories.Abstract
{
    public interface IProductRepository:IBaseRepository<ProductDto,ProductAddDto,ProductUpdateDto>
    {
        Task<List<ProductDto>> GetProductByNameAsync(string name);
        Task<List<ProductDto>> GetProductByCategoryIdAsync(int categoryId);
    }
}
