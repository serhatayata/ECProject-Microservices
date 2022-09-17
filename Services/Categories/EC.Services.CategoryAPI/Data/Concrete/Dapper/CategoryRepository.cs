using Dapper;
using EC.Services.CategoryAPI.Data.Abstract.Dapper;
using EC.Services.CategoryAPI.Entities;
using Microsoft.Data.SqlClient;
using Nest;
using System.ComponentModel.Design;
using System.Drawing.Printing;

namespace EC.Services.CategoryAPI.Data.Concrete.Dapper
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConfiguration _configuration;

        public CategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region AnyAsync
        public async Task<bool> AnyAsync()
        {
            var sql = "SELECT TOP 1 * FROM Categories";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>(sql, new { Node = 0 });
                if (result.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region AnyByLinkAsync
        public async Task<bool> AnyByLinkAsync(string link)
        {
            var sql = "SELECT TOP 1 * FROM Categories WHERE Link=@Link";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>(sql, new { Link = link });
                if (result.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region GetLastLineAsync
        public async Task<Category> GetLastLineAsync(int upperCategoryId)
        {
            var sql = "SELECT * FROM Categories WHERE ParentId=@ParentId ORDER BY Id DESC";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Category>(sql, new { ParentId=upperCategoryId });
                return result;
            }
        }
        #endregion
        #region GetAllAsync
        public async Task<List<Category>> GetAllAsync()
        {
            var sql = "SELECT * FROM Categories";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>(sql);
                return result.ToList();
            }
        }
        #endregion
        #region GetAllSubCategoriesByIdAsync
        public async Task<List<Category>> GetAllSubCategoriesByIdAsync(int id)
        {
            var sql = "SELECT * FROM Categories WHERE ParentId=@ParentId";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>(sql, new { ParentId = id });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllSubCategoriesByIdPagingAsync
        public async Task<List<Category>> GetAllSubCategoriesByIdPagingAsync(int id, int page = 1, int pageSize = 8)
        {
            var sql = "SELECT * FROM Categories WHERE ParentId=@ParentId ORDER BY Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>(sql, new { ParentId = id, Page=page, PageSize=pageSize });
                return result.ToList();
            }
        }
        #endregion
        #region GetByIdAsync
        public async Task<Category> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Categories WHERE Id=@Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Category>(sql, new { Id = id });
                return result;
            }
        }
        #endregion
        #region GetByLinkAsync
        public async Task<Category> GetByLinkAsync(string link)
        {
            var sql = "SELECT * FROM Categories WHERE Link=@Link";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Category>(sql, new { Link = link });
                return result;
            }
        }
        #endregion
        #region GetByNameAsync
        public async Task<List<Category>> GetByNameAsync(string name)
        {
            var sql = "SELECT * FROM Categories WHERE Name LIKE @Name";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>(sql, new { Name= $"%{name}%" });
                return result.ToList();
            }
        }
        #endregion
        #region GetByNamePagingAsync
        public async Task<List<Category>> GetByNamePagingAsync(string name, int page = 1, int pageSize = 8)
        {
            var sql = "SELECT * FROM Categories WHERE Name LIKE @Name ORDER BY Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>(sql, new { Name = $"%{name}%", Page=page, PageSize=pageSize });
                return result.ToList();
            }
        }
        #endregion


    }
}
