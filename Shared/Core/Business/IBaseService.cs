using Core.Utilities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Services.Abstract
{
    public interface IBaseService<T, C, U, D>
    {
        Task<DataResult<List<T>>> GetAllAsync();
        Task<DataResult<T>> CreateAsync(C entity);
        Task<DataResult<T>> UpdateAsync(U entity);
        Task<IResult> DeleteAsync(D entity);
    }
}
