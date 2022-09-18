﻿using Core.Utilities.Results;
using EC.Services.PhotoStockAPI.Dtos;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.PhotoStockAPI.Services.Abstract
{
    public interface IPhotoService
    {
        Task<IResult> AddAsync(PhotoAddDto model);
        Task<DataResult<PhotoDto>> GetByIdAsync(int id);
        Task<DataResult<PhotoDto>> GetAllByTypeAsync(PhotoGetAllByTypeDto model);
        Task<DataResult<PhotoDto>> GetAllByTypeAndEntityIdAsync(PhotoGetAllByTypeAndEntityIdDto model);
        Task<DataResult<PhotoDto>> GetAllAsync();
        Task<IResult> DeleteAllByTypeAndEntityIdAsync(PhotoDeleteDto model);
        Task<IResult> DeleteByIdAsync(int id);
    }
}
