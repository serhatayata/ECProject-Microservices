using Core.Utilities.Results;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Dtos.VariantDtos;

namespace EC.Services.ProductAPI.Repositories.Abstract
{
    public interface IVariantRepository : IBaseRepository<VariantDto, VariantAddDto, VariantUpdateDto>
    {
        Task<DataResult<List<VariantDto>>> GetAllPagingAsync(int page = 1, int pageSize = 8);

    }
}
