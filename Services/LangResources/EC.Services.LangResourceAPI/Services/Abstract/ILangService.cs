﻿using Core.Dtos;
using Core.Utilities.Results;
using EC.Services.LangResourceAPI.Dtos.LangDtos;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.LangResourceAPI.Services.Abstract
{
    public interface ILangService
    {
        Task<DataResult<List<LangDto>>> GetAllAsync();
        Task<DataResult<List<LangDto>>> GetAllPagingAsync(PagingDto model);

        Task<DataResult<LangDto>> GetByCodeAsync(string code);

        Task<IResult> AddAsync(LangAddDto model);
        Task<IResult> UpdateAsync(LangUpdateDto model);
        Task<IResult> DeleteAsync(DeleteIntDto model);


    }
}
