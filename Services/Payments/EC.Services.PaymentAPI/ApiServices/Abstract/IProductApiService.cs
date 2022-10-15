using Core.Utilities.Results;
using EC.Services.PaymentAPI.ApiDtos;
using EC.Services.PaymentAPI.Dtos.ProductDtos;

namespace EC.Services.PaymentAPI.ApiServices.Abstract
{
    public interface IProductApiService
    {
        Task<DataResult<List<ProductApiDto>>> GetProductsByProductIdsAsync(ProductGetByProductIdsDto model);
    }
}
