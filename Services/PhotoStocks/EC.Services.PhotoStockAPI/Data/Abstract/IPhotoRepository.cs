using Core.DataAccess.Dapper;
using EC.Services.PhotoStockAPI.Entities;

namespace EC.Services.PhotoStockAPI.Data.Abstract
{
    public interface IPhotoRepository:IGenericRepository<Photo>
    {
        Task<List<Photo>> GetAllByTypeAndEntityIdAsync(int type, int entityId);
        Task<List<Photo>> GetAllByTypeAsync(int type);
        Task<bool> DeleteAllByTypeAndEntityIdAsync(int type,int entityId);
    }
}
