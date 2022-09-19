using Core.DataAccess.Dapper;
using Dapper;
using EC.Services.PhotoStockAPI.Data.Abstract;
using EC.Services.PhotoStockAPI.Dtos;
using EC.Services.PhotoStockAPI.Entities;
using Microsoft.Data.SqlClient;
using Nest;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EC.Services.PhotoStockAPI.Data.Concrete
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDapperManager _dapperManager;

        public PhotoRepository(IConfiguration configuration, IDapperManager dapperManager)
        {
            _configuration = configuration;
            _dapperManager = dapperManager;
        }

        #region AnyAsync
        public async Task<bool> AnyAsync()
        {
            var sql = "SELECT TOP 1 * FROM Photos";
            var result = await _dapperManager.GetAsync(sql,new DynamicParameters());
        }
        #endregion
        #region GetAllAsync
        public async Task<List<Photo>> GetAllAsync()
        {

        }
        #endregion
        #region GetAllByTypeAndEntityIdAsync
        public async Task<List<Photo>> GetAllByTypeAndEntityIdAsync(int type, int entityId)
        {

        }
        #endregion
        #region GetAllByTypeAsync
        public async Task<List<Photo>> GetAllByTypeAsync(int type)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetByIdAsync
        public async Task<Photo> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region DeleteAllByTypeAndEntityIdAsync
        public async Task<bool> DeleteAllByTypeAndEntityIdAsync(int type,int entityId)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
