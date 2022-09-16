using Core.Utilities.Results;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Dtos.StockDtos;
using EC.Services.ProductAPI.Dtos.VariantDtos;

namespace EC.Services.ProductAPI.Repositories.Abstract
{
    public interface IStockRepository : IBaseRepository<StockDto, StockAddDto, StockUpdateDto>
    {
        Task<DataResult<StockDto>> GetByProductIdAsync(string productId);
        Task<DataResult<List<StockDto>>> GetAllPagingAsync(int page = 1, int pageSize = 8);
    }
}
