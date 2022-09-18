using Core.Utilities.Results;
using EC.Services.PhotoStockAPI.Dtos;
using EC.Services.PhotoStockAPI.Services.Abstract;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.PhotoStockAPI.Services.Concrete
{
    public class PhotoManager : IPhotoService
    {


        public Task<IResult> AddAsync(PhotoAddDto model)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> DeleteAllByTypeAndEntityIdAsync(PhotoDeleteDto model)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<PhotoDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<PhotoDto>> GetAllByTypeAndEntityIdAsync(PhotoGetAllByTypeAndEntityIdDto model)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<PhotoDto>> GetAllByTypeAsync(PhotoGetAllByTypeDto model)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<PhotoDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
