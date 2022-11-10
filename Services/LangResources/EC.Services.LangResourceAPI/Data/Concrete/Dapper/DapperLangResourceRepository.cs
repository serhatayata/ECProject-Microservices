using Core.Dtos;
using Core.Utilities.Results;
using Dapper;
using EC.Services.LangResourceAPI.Data.Abstract.Dapper;
using EC.Services.LangResourceAPI.Dtos.LangResourceDtos;
using EC.Services.LangResourceAPI.Entities;
using Microsoft.Data.SqlClient;
using Nest;

namespace EC.Services.LangResourceAPI.Data.Concrete.Dapper
{
    public class DapperLangResourceRepository : IDapperLangResourceRepository
    {
        private readonly IConfiguration _configuration;
        private string _defaultConnection;

        public DapperLangResourceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _defaultConnection = _configuration.GetConnectionString("DefaultConnection");
        }

        #region AnyAsync
        public async Task<bool> AnyAsync()
        {
            var sql = "SELECT TOP 1 * FROM LangResources";
            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<LangResource>(sql);
                if (result.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region GetAllAsync
        public async Task<List<LangResource>> GetAllAsync()
        {
            var sql = "SELECT lr.Id,lr.Tag,lr.Description,lr.MessageCode,lr.LangId, " +
                      "l.Id,l.Name,l.Code " +
                      "FROM LangResources lr LEFT JOIN Langs l " +
                      "ON lr.LangId = l.Id";

            var dict = new Dictionary<int, LangResource>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<LangResource, Lang, LangResource>(sql,
                    (lr, l) =>
                    {
                        lr.Lang = l;
                        return lr;
                    },splitOn:"Id");
                return result.Distinct().ToList();
            }
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<List<LangResource>> GetAllPagingAsync(PagingDto model)
        {
            var sql = "SELECT lr.Id,lr.Tag,lr.Description,lr.MessageCode,lr.LangId, " +
                      "l.Id,l.Name,l.Code " +
                      "FROM LangResources lr LEFT JOIN Langs l " +
                      "ON lr.LangId = l.Id " +
                      "ORDER BY lr.Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";

            var dict = new Dictionary<int, LangResource>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<LangResource, Lang, LangResource>(sql,
                    (lr, l) =>
                    {
                        lr.Lang = l;
                        return lr;
                    },new { Page=model.Page-1, PageSize=model.PageSize }, splitOn: "Id");
                return result.Distinct().ToList();
            }
        }
        #endregion
        #region GetAllByLangIdPagingAsync
        public async Task<List<LangResource>> GetAllByLangIdPagingAsync(LangResourceGetAllByLangIdPagingDto model)
        {
            var sql = "SELECT lr.Id,lr.Tag,lr.Description,lr.MessageCode,lr.LangId, " +
                      "l.Id,l.Name,l.Code " +
                      "FROM LangResources lr LEFT JOIN Langs l " +
                      "ON lr.LangId = l.Id " +
                      "ORDER BY lr.Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";

            var dict = new Dictionary<int, LangResource>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<LangResource, Lang, LangResource>(sql,
                    (lr, l) =>
                    {
                        lr.Lang = l;
                        return lr;
                    }, new { LangId=model.LangId, Page = model.Page - 1, PageSize = model.PageSize }, splitOn: "Id");
                return result.Distinct().ToList();
            }
        }
        #endregion
        #region GetByIdAsync
        public async Task<LangResource> GetByIdAsync(int id)
        {
            var sql = "SELECT lr.Id,lr.Tag,lr.Description,lr.MessageCode,lr.LangId, " +
                      "l.Id,l.Name,l.Code " +
                      "FROM LangResources lr LEFT JOIN Langs l " +
                      "ON lr.LangId = l.Id " +
                      "WHERE lr.Id=@Id";

            var dict = new Dictionary<int, LangResource>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<LangResource, Lang, LangResource>(sql,
                    (lr, l) =>
                    {
                        lr.Lang = l;
                        return lr;
                    }, new { Id=id }, splitOn: "Id");
                return result?.FirstOrDefault();
            }
        }
        #endregion
        #region GetByMessageCodeAsync
        public async Task<LangResource> GetByMessageCodeAsync(string messageCode)
        {
            var sql = "SELECT lr.Id,lr.Tag,lr.Description,lr.MessageCode,lr.LangId, " +
                      "l.Id,l.Name,l.Code " +
                      "FROM LangResources lr LEFT JOIN Langs l " +
                      "ON lr.LangId = l.Id " +
                      "WHERE lr.MessageCode=@MessageCode";

            var dict = new Dictionary<int, LangResource>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<LangResource, Lang, LangResource>(sql,
                    (lr, l) =>
                    {
                        lr.Lang = l;
                        return lr;
                    }, new { MessageCode = messageCode }, splitOn: "Id");
                return result?.FirstOrDefault();
            }
        }
        #endregion

    }
}
