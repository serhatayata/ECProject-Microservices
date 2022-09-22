using Core.Utilities.Results;
using EC.Services.PhotoStockAPI.Dtos;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.PhotoStockAPI.Services.Abstract
{
    public interface IPhotoService
    {
        Task<DataResult<string>> AddAsync(PhotoAddDto model);
        Task<DataResult<PhotoDto>> GetByIdAsync(int id);
        Task<DataResult<List<PhotoDto>>> GetAllByTypeAsync(PhotoGetAllByTypeDto model);
        Task<DataResult<List<PhotoDto>>> GetAllByTypeAndEntityIdAsync(PhotoGetAllByTypeAndEntityIdDto model);
        Task<DataResult<List<PhotoDto>>> GetAllAsync();
        Task<IResult> DeleteAllByTypeAndEntityIdAsync(PhotoDeleteDto model);
        Task<IResult> DeleteByIdAsync(int id);
        Task<IResult> DeleteByUrlAsync(string url);
    }
}
