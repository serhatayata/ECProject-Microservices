using AutoMapper;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.LangResourceAPI.Constants;
using EC.Services.LangResourceAPI.Data.Abstract.Dapper;
using EC.Services.LangResourceAPI.Data.Abstract.EntityFramework;
using EC.Services.LangResourceAPI.Data.Concrete.Dapper;
using EC.Services.LangResourceAPI.Dtos.LangDtos;
using EC.Services.LangResourceAPI.Dtos.LangResourceDtos;
using EC.Services.LangResourceAPI.Entities;
using EC.Services.LangResourceAPI.Services.Abstract;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.LangResourceAPI.Services.Concrete
{
    public class LangService : ILangService
    {
        private readonly IEfLangRepository _efLangRepository;
        private readonly IDapperLangRepository _dapperLangRepository;
        private readonly IMapper _mapper;

        public LangService(IEfLangRepository efLangRepository, IDapperLangRepository dapperLangRepository, IMapper mapper)
        {
            _efLangRepository = efLangRepository;
            _dapperLangRepository = dapperLangRepository;
            _mapper = mapper;
        }

        #region AddAsync
        public async Task<DataResult<LangDto>> AddAsync(LangAddDto model)
        {
            var codeExists = await _efLangRepository.GetAsync(x => x.Code == model.Code);
            if (codeExists != null)
            {
                return new ErrorDataResult<LangDto>(MessageExtensions.AlreadyExists(LangResourceConstantValues.LangResourceLangCode));
            }
            var langAdded = _mapper.Map<Lang>(model);
            await _efLangRepository.AddAsync(langAdded);
            var langExists = await _efLangRepository.GetAsync(x => x.Id == langAdded.Id);
            if (langExists == null)
            {
                return new ErrorDataResult<LangDto>(MessageExtensions.NotAdded(LangResourceConstantValues.LangResourceLang));
            }
            var langResult = _mapper.Map<LangDto>(langExists);
            return new SuccessDataResult<LangDto>(langResult);
        }
        #endregion
        #region UpdateAsync
        public async Task<DataResult<LangDto>> UpdateAsync(LangUpdateDto model)
        {
            var codeExists = await _efLangRepository.GetAsync(x => x.Id != model.Id && x.Code == model.Code);
            if (codeExists != null)
            {
                return new ErrorDataResult<LangDto>(MessageExtensions.AlreadyExists(LangResourceConstantValues.LangResourceLangCode));
            }
            var langUpdated = _mapper.Map<Lang>(model);
            await _efLangRepository.UpdateAsync(langUpdated);

            var langResult = _mapper.Map<LangDto>(langUpdated);
            return new SuccessDataResult<LangDto>(langResult);
        }
        #endregion
        #region DeleteAsync
        public async Task<IResult> DeleteAsync(DeleteIntDto model)
        {
            var langExists = await _efLangRepository.GetAsync(x => x.Id == model.Id);
            if (langExists == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(LangResourceConstantValues.LangResourceLang));
            }
            await _efLangRepository.DeleteAsync(langExists);
            var langDeleted = await _efLangRepository.GetAsync(x => x.Id == langExists.Id);
            if (langDeleted == null)
            {
                return new SuccessResult(MessageExtensions.Deleted(LangResourceConstantValues.LangResourceLang));
            }
            return new ErrorResult(MessageExtensions.NotDeleted(LangResourceConstantValues.LangResource), StatusCodes.Status500InternalServerError);
        }
        #endregion
        #region GetAllAsync
        public async Task<DataResult<List<LangDto>>> GetAllAsync()
        {
            var langs = await _dapperLangRepository.GetAllAsync();
            if (langs == null || langs!.Count < 1)
            {
                return new ErrorDataResult<List<LangDto>>(MessageExtensions.NotFound(LangResourceConstantValues.LangResource));
            }
            var langResult = _mapper.Map<List<LangDto>>(langs);
            return new SuccessDataResult<List<LangDto>>(langResult);
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<DataResult<List<LangDto>>> GetAllPagingAsync(PagingDto model)
        {
            var langs = await _dapperLangRepository.GetAllPagingAsync(model);
            if (langs == null || langs!.Count < 1)
            {
                return new ErrorDataResult<List<LangDto>>(MessageExtensions.NotFound(LangResourceConstantValues.LangResource));
            }
            var langResult = _mapper.Map<List<LangDto>>(langs);
            return new SuccessDataResult<List<LangDto>>(langResult);
        }
        #endregion
        #region GetByCodeAsync
        public async Task<DataResult<LangDto>> GetByCodeAsync(string code)
        {
            var lang = await _dapperLangRepository.GetByCodeAsync(code);
            if (lang == null)
            {
                return new ErrorDataResult<LangDto>(MessageExtensions.NotFound(LangResourceConstantValues.LangResourceLang));
            }
            var langResult = _mapper.Map<LangDto>(lang);
            return new SuccessDataResult<LangDto>(langResult);
        }
        #endregion

    }
}
