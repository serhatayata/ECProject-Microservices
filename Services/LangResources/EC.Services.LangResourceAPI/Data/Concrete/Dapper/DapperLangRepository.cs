using Core.Dtos;
using Core.Utilities.Results;
using Dapper;
using EC.Services.LangResourceAPI.Data.Abstract.Dapper;
using EC.Services.LangResourceAPI.Entities;
using Elasticsearch.Net;
using Microsoft.Data.SqlClient;
using Nest;
using System.Drawing.Printing;

namespace EC.Services.LangResourceAPI.Data.Concrete.Dapper
{
    public class DapperLangRepository : IDapperLangRepository
    {
        private readonly IConfiguration _configuration;
        private string _defaultConnection;

        public DapperLangRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _defaultConnection = _configuration.GetConnectionString("DefaultConnection");
        }

        #region AnyAsync
        public async Task<bool> AnyAsync()
        {
            var sql = "SELECT TOP 1 * FROM Langs";
            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Lang>(sql);
                if (result.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region GetAllAsync
        public async Task<List<Lang>> GetAllAsync()
        {
            var sql = "SELECT l.Id,l.DisplayName,l.Name,l.Code, " +
                      "lr.Id,lr.Tag,lr.Description,lr.MessageCode,lr.LangId " +
                      "FROM Langs l LEFT JOIN LangResources lr " +
                      "ON l.Id = lr.LangId";


            var dict = new Dictionary<int, Lang>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Lang, LangResource, Lang>(sql,
                    (l, lr) =>
                    {
                        Lang lang;
                        if (!dict.TryGetValue(l.Id, out lang))
                        {
                            lang = l;
                            lang.LangResources = new List<LangResource>();
                            dict.Add(l.Id, lang);
                        }
                        if (lr.LangId > 0)
                        {
                            lang.LangResources.Add(lr);
                        }
                        return lang;
                    }, splitOn: "Id");
                return result.Distinct().ToList();
            }
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<List<Lang>> GetAllPagingAsync(PagingDto model)
        {
            var sql = "SELECT l.Id,l.DisplayName,l.Name,l.Code, " +
                      "lr.Id,lr.Tag,lr.Description,lr.MessageCode,lr.LangId " +
                      "FROM Langs l LEFT JOIN LangResources lr " +
                      "ON l.Id = lr.LangId " +
                      "WHERE l.Code IN " +
                      "(SELECT l.Code FROM Langs l ORDER BY l.Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY)";

            var dict = new Dictionary<int, Lang>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Lang, LangResource, Lang>(sql,
                    (l, lr) =>
                    {
                        Lang lang;
                        if (!dict.TryGetValue(l.Id, out lang))
                        {
                            lang = l;
                            lang.LangResources = new List<LangResource>();
                            dict.Add(l.Id, lang);
                        }
                        if (lr.LangId > 0)
                        {
                            lang.LangResources.Add(lr);
                        }
                        return lang;
                    }, new { Page = model.Page - 1, PageSize = model.PageSize }, splitOn: "Id");
                return result.Distinct().ToList();
            }
        }
        #endregion
        #region GetByCodeAsync
        public async Task<Lang> GetByCodeAsync(string code)
        {
            var sql = "SELECT l.Id,l.DisplayName,l.Name,l.Code, " +
                      "lr.Id,lr.Tag,lr.Description,lr.MessageCode,lr.LangId " +
                      "FROM Langs l LEFT JOIN LangResources lr " +
                      "ON l.Id = lr.LangId " +
                      "WHERE l.Code=@Code";


            var dict = new Dictionary<int, Lang>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Lang, LangResource, Lang>(sql,
                    (l, lr) =>
                    {
                        Lang lang;
                        if (!dict.TryGetValue(l.Id, out lang))
                        {
                            lang = l;
                            lang.LangResources = new List<LangResource>();
                            dict.Add(l.Id, lang);
                        }
                        if (lr.LangId > 0)
                        {
                            lang.LangResources.Add(lr);
                        }
                        return lang;
                    }, new { Code = code }, splitOn: "Id");
                return result?.FirstOrDefault();
            }
        }
        #endregion
        #region GetByIdAsync
        public async Task<Lang> GetByIdAsync(int id)
        {
            var sql = "SELECT l.Id,l.DisplayName,l.Name,l.Code, " +
                      "lr.Id,lr.Tag,lr.Description,lr.MessageCode,lr.LangId " +
                      "FROM Langs l LEFT JOIN LangResources lr " +
                      "ON l.Id = lr.LangId " +
                      "WHERE l.Id=@Id";


            var dict = new Dictionary<int, Lang>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Lang, LangResource, Lang>(sql,
                    (l, lr) =>
                    {
                        Lang lang;
                        if (!dict.TryGetValue(l.Id, out lang))
                        {
                            lang = l;
                            lang.LangResources = new List<LangResource>();
                            dict.Add(l.Id, lang);
                        }
                        if (lr.LangId > 0)
                        {
                            lang.LangResources.Add(lr);
                        }
                        return lang;
                    }, new { Id = id }, splitOn: "Id");
                return result?.FirstOrDefault();
            }
        }
        #endregion
        #region GetByDisplayNameAsync
        public async Task<Lang> GetByDisplayNameAsync(string displayName)
        {
            var sql = "SELECT l.Id,l.DisplayName,l.Name,l.Code, " +
                      "lr.Id,lr.Tag,lr.Description,lr.MessageCode,lr.LangId " +
                      "FROM Langs l LEFT JOIN LangResources lr " +
                      "ON l.Id = lr.LangId " +
                      "WHERE l.DisplayName=@DisplayName";

            var dict = new Dictionary<int, Lang>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Lang, LangResource, Lang>(sql,
                    (l, lr) =>
                    {
                        Lang lang;
                        if (!dict.TryGetValue(l.Id, out lang))
                        {
                            lang = l;
                            lang.LangResources = new List<LangResource>();
                            dict.Add(l.Id, lang);
                        }
                        if (lr.LangId > 0)
                        {
                            lang.LangResources.Add(lr);
                        }
                        return lang;
                    }, new { DisplayName = displayName }, splitOn: "Id");
                return result?.FirstOrDefault();
            }
        }
        #endregion

    }
}
