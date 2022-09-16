using Core.Utilities.Results;
using EC.Services.ProductAPI.Entities;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.ProductAPI.Repositories.Abstract
{
    public interface IBaseRepository<T,C,U>
    {
        Task<DataResult<T>> GetAsync(string id);
        Task<DataResult<List<T>>> GetAllAsync();
        Task<IResult> CreateAsync(C entity);
        Task<IResult> UpdateAsync(U entity);
        Task<IResult> DeleteAsync(string id);
    }
}
