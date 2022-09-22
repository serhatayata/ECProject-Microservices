using AutoMapper;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.PhotoStockAPI.Constants;
using EC.Services.PhotoStockAPI.Data.Abstract.Dapper;
using EC.Services.PhotoStockAPI.Data.Abstract.EntityFramework;
using EC.Services.PhotoStockAPI.Dtos;
using EC.Services.PhotoStockAPI.Entities;
using EC.Services.PhotoStockAPI.Extensions;
using EC.Services.PhotoStockAPI.Services.Abstract;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.PhotoStockAPI.Services.Concrete
{
    public class PhotoManager : IPhotoService
    {
        private readonly IDapperPhotoRepository _dapperPhotoRepository;
        private readonly IEfPhotoRepository _efPhotoRepository;
        private readonly IMapper _mapper;

        public PhotoManager(IDapperPhotoRepository dapperPhotoRepository, IEfPhotoRepository efPhotoRepository, IMapper mapper)
        {
            _dapperPhotoRepository = dapperPhotoRepository;
            _efPhotoRepository = efPhotoRepository;
            _mapper = mapper;
        }

        #region AddAsync
        public async Task<DataResult<string>> AddAsync(PhotoAddDto model)
        {
            if (model.Photo == null || model.Photo?.FileName == null || model.Photo.Length <= 0)
            {
                return new ErrorDataResult<string>(MessageExtensions.NotFound(PhotoTitles.Photo));
            }

            var timeStamp = DateExtensions.GetTimestamp(DateTime.Now);
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + timeStamp;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", uniqueFileName);

            using var stream = new FileStream(path, FileMode.Create);
            await model.Photo.CopyToAsync(stream);

            if (File.Exists(path))
            {
                var photoAdded = _mapper.Map<Photo>(model);
                photoAdded.Url = uniqueFileName;
                await _efPhotoRepository.AddAsync(photoAdded);

                if (await _dapperPhotoRepository.GetByIdAsync(photoAdded.Id) == null)
                {
                    System.IO.File.Delete(path);
                    return new ErrorDataResult<string>(MessageExtensions.NotAdded(PhotoTitles.Photo));
                }

                return new SuccessDataResult<string>(uniqueFileName);
            }
            return new ErrorDataResult<string>(MessageExtensions.NotAdded(PhotoTitles.Photo));
        }
        #endregion
        #region DeleteByIdAsync
        public async Task<IResult> DeleteByIdAsync(int id)
        {
            var photoExists = await _dapperPhotoRepository.GetByIdAsync(id);
            if (photoExists == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(PhotoTitles.Photo));
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoExists.Url);

            await _efPhotoRepository.DeleteAsync(photoExists);
            if (await _dapperPhotoRepository.GetByIdAsync(photoExists.Id) != null)
            {
                return new ErrorDataResult<string>(MessageExtensions.NotDeleted(PhotoTitles.Photo));
            }

            System.IO.File.Delete(path);

            return new ErrorResult(MessageExtensions.NotDeleted(PhotoTitles.Photo));
        }
        #endregion
        #region DeleteByUrlAsync
        public async Task<IResult> DeleteByUrlAsync(string url)
        {
            var photoExists = await _efPhotoRepository.GetAsync(x => x.Url == url);
            if (photoExists == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(PhotoTitles.Photo));
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoExists.Url);

            await _efPhotoRepository.DeleteAsync(photoExists);
            if (await _dapperPhotoRepository.GetByIdAsync(photoExists.Id) != null)
            {
                return new ErrorDataResult<string>(MessageExtensions.NotDeleted(PhotoTitles.Photo));
            }

            System.IO.File.Delete(path);

            return new ErrorResult(MessageExtensions.NotDeleted(PhotoTitles.Photo));
        }
        #endregion
        #region DeleteAllByTypeAndEntityIdAsync
        public async Task<IResult> DeleteAllByTypeAndEntityIdAsync(PhotoDeleteDto model)
        {
            var allPhotos = await _dapperPhotoRepository.GetAllByTypeAndEntityIdAsync(model.Type, model.EntityId);
            if (allPhotos?.Count == 0 || allPhotos == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(PhotoTitles.Photo));
            }

            foreach (var photo in allPhotos)
            {
                await _efPhotoRepository.DeleteAsync(photo);
                if (await _dapperPhotoRepository.GetByIdAsync(photo.Id) == null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.Url);

                    System.IO.File.Delete(path);
                }
            }



        }
        #endregion
                #region GetByIdAsync
        public async Task<DataResult<PhotoDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllAsync
        public async Task<DataResult<List<PhotoDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllByTypeAndEntityIdAsync
        public async Task<DataResult<List<PhotoDto>>> GetAllByTypeAndEntityIdAsync(PhotoGetAllByTypeAndEntityIdDto model)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllByTypeAsync
        public async Task<DataResult<List<PhotoDto>>> GetAllByTypeAsync(PhotoGetAllByTypeDto model)
        {
            throw new NotImplementedException();
        }
        #endregion



    }
}
