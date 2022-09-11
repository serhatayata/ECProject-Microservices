using Core.Utilities.Results;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Dtos.ProductVariantDtos;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.ProductAPI.Repositories.Abstract
{
    public interface IProductVariantRepository : IBaseRepository<ProductVariantDto, ProductVariantDto, ProductVariantDto>
    {
        Task<IResult> DeleteByProductAndVariantId(string productId,string variantId);
        Task<DataResult<ProductVariantDto>> GetByProductAndVariantId(string productId,string variantId);
    }
}
