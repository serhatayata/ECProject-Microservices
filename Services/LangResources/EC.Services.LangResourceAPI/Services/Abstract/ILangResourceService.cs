using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.LangResourceAPI.Dtos.LangDtos;
using EC.Services.LangResourceAPI.Dtos.LangResourceDtos;
using EC.Services.LangResourceAPI.Entities;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.LangResourceAPI.Services.Abstract
{
    public interface ILangResourceService
    {
        Task<DataResult<List<LangResourceDto>>> GetAllAsync();
        Task<DataResult<List<LangResourceDto>>> GetAllPagingAsync(PagingDto model);

        Task<DataResult<List<LangResourceDto>>> GetAllByLangIdAsync(int langId);

        Task<DataResult<LangResourceDto>> GetByMessageCodeAsync(string messageCode);

        Task<DataResult<LangResourceDto>> AddAsync(LangResourceAddDto model);
        Task<DataResult<LangResourceDto>> UpdateAsync(LangResourceUpdateDto model);
        Task<IResult> DeleteAsync(int id);
        Task<IResult> RefreshAsync();

    }
}
