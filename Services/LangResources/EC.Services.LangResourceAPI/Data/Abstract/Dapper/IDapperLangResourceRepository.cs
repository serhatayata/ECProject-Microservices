using Core.DataAccess;
using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.LangResourceAPI.Dtos.LangResourceDtos;
using EC.Services.LangResourceAPI.Entities;

namespace EC.Services.LangResourceAPI.Data.Abstract.Dapper
{
    public interface IDapperLangResourceRepository:IGenericRepository<LangResource>
    {
        Task<List<LangResource>> GetAllPagingAsync(PagingDto model);
        Task<List<LangResource>> GetAllByLangIdPagingAsync(LangResourceGetAllByLangIdPagingDto model);
        Task<List<LangResource>> GetAllByLangIdAsync(int langId);

        Task<LangResource> GetByMessageCodeAsync(string messageCode);
    }
}
