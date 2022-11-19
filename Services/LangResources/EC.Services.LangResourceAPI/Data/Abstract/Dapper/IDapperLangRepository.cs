using Core.DataAccess.Dapper;
using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.LangResourceAPI.Dtos.LangDtos;
using EC.Services.LangResourceAPI.Entities;

namespace EC.Services.LangResourceAPI.Data.Abstract.Dapper
{
    public interface IDapperLangRepository:IGenericRepository<Lang>
    {
        Task<List<Lang>> GetAllPagingAsync(PagingDto model);

        Task<Lang> GetByCodeAsync(string code);
        Task<Lang> GetByDisplayNameAsync(string displayName);
    }
}
