using Core.Utilities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Services.Abstract
{
    public interface IBaseRepository<T, C, U, D>
    {
        Task<DataResult<T>> GetAsync(string id);
        Task<DataResult<List<T>>> GetAllAsync();
        Task<IResult> CreateAsync(C entity);
        Task<IResult> UpdateAsync(U entity);
        Task<IResult> DeleteAsync(D entity);
    }
}
