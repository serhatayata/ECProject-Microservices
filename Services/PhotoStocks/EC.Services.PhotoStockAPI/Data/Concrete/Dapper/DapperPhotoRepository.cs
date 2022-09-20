using Core.DataAccess.Dapper;
using Dapper;
using EC.Services.PhotoStockAPI.Data.Abstract;
using EC.Services.PhotoStockAPI.Data.Abstract.Dapper;
using EC.Services.PhotoStockAPI.Dtos;
using EC.Services.PhotoStockAPI.Entities;
using Microsoft.Data.SqlClient;
using Nest;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EC.Services.PhotoStockAPI.Data.Concrete.Dapper
{
    public class DapperPhotoRepository : IDapperPhotoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDapperManager _dapperManager;

        public DapperPhotoRepository(IConfiguration configuration, IDapperManager dapperManager)
        {
            _configuration = configuration;
            _dapperManager = dapperManager;
        }

        #region AnyAsync
        public async Task<bool> AnyAsync()
        {
            var sql = "SELECT TOP 1 * FROM Photos";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Photo>(sql);
                if (result.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region GetAllAsync
        public async Task<List<Photo>> GetAllAsync()
        {
            var sql = "SELECT * FROM Photos";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Photo>(sql);
                return result.ToList();
            }
        }
        #endregion
        #region GetAllByTypeAndEntityIdAsync
        public async Task<List<Photo>> GetAllByTypeAndEntityIdAsync(int photoType, int entityId)
        {
            var sql = "SELECT * FROM Photos WHERE PhotoType=@PhotoType AND EntityId=@EntityId";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Photo>(sql, new { PhotoType = photoType, EntityId = entityId });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllByTypeAsync
        public async Task<List<Photo>> GetAllByTypeAsync(int photoType)
        {
            var sql = "SELECT * FROM Photos WHERE PhotoType=@PhotoType";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Photo>(sql, new { PhotoType = photoType });
                return result.ToList();
            }
        }
        #endregion
        #region GetByIdAsync
        public async Task<Photo> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Photos WHERE Id=@Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Photo>(sql, new { Id = id });
                return result;
            }
        }
        #endregion
        #region DeleteAllByTypeAndEntityIdAsync
        public async Task<bool> DeleteAllByTypeAndEntityIdAsync(int photoType, int entityId)
        {
            var sql = "DELETE FROM Photos WHERE PhotoType = @PhotoType AND EntityId=@EntityId";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, new { PhotoType = photoType, EntityId= entityId });
                string sqlDeleted = "SELECT * FROM Photos WHERE PhotoType=@PhotoType AND EntityId=@EntityId";
                var result = await connection.QueryFirstOrDefaultAsync<Photo>(sqlDeleted, new { PhotoType = photoType, EntityId=entityId});
                return result == null ? true : false;
            }
        }
        #endregion

    }
}
