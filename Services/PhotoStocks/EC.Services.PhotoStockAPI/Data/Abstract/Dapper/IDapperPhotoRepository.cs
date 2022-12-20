using Core.DataAccess;
using EC.Services.PhotoStockAPI.Entities;

namespace EC.Services.PhotoStockAPI.Data.Abstract.Dapper
{
    public interface IDapperPhotoRepository : IGenericRepository<Photo>
    {
        Task<List<Photo>> GetAllByTypeAndEntityIdAsync(int photoType, int entityId);
        Task<List<Photo>> GetAllByTypeAsync(int photoType);
        Task<bool> DeleteAllByTypeAndEntityIdAsync(int photoType, int entityId);
    }
}
