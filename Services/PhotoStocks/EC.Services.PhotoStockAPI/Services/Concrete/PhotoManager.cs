using Core.Utilities.Results;
using EC.Services.PhotoStockAPI.Dtos;
using EC.Services.PhotoStockAPI.Services.Abstract;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.PhotoStockAPI.Services.Concrete
{
    public class PhotoManager : IPhotoService
    {


        public async Task<IResult> AddAsync(PhotoAddDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> DeleteAllByTypeAndEntityIdAsync(PhotoDeleteDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult<List<PhotoDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult<List<PhotoDto>>> GetAllByTypeAndEntityIdAsync(PhotoGetAllByTypeAndEntityIdDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult<List<PhotoDto>>> GetAllByTypeAsync(PhotoGetAllByTypeDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult<PhotoDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
