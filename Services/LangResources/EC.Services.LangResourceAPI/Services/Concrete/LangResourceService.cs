using AutoMapper;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.LangResourceAPI.Constants;
using EC.Services.LangResourceAPI.Data.Abstract.Dapper;
using EC.Services.LangResourceAPI.Data.Abstract.EntityFramework;
using EC.Services.LangResourceAPI.Dtos.LangResourceDtos;
using EC.Services.LangResourceAPI.Entities;
using EC.Services.LangResourceAPI.Services.Abstract;
using Nest;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.LangResourceAPI.Services.Concrete
{
    public class LangResourceService : ILangResourceService
    {
        private readonly IEfLangResourceRepository _efLangResourceRepository;
        private readonly IDapperLangResourceRepository _dapperLangResourceRepository;
        private readonly IMapper _mapper;

        public LangResourceService(IEfLangResourceRepository efLangResourceRepository, IDapperLangResourceRepository dapperLangResourceRepository,IMapper mapper)
        {
            _efLangResourceRepository = efLangResourceRepository;
            _dapperLangResourceRepository = dapperLangResourceRepository;
            _mapper = mapper;
        }

        #region AddAsync
        public async Task<DataResult<LangResourceDto>> AddAsync(LangResourceAddDto model)
        {
            var messageCodeExists = await _efLangResourceRepository.GetAsync(x => x.MessageCode == model.MessageCode);
            if (messageCodeExists != null)
            {
                return new ErrorDataResult<LangResourceDto>(MessageExtensions.AlreadyExists(LangResourceConstantValues.LangResourceMessageCode),StatusCodes.Status400BadRequest);
            }
            var langResourceAdded = _mapper.Map<LangResource>(model);
            await _efLangResourceRepository.AddAsync(langResourceAdded);
            var langResourceExists = _efLangResourceRepository.GetAsync(x => x.Id == langResourceAdded.Id);
            if (langResourceAdded == null)
            {
                return new ErrorDataResult<LangResourceDto>(MessageExtensions.NotAdded(LangResourceConstantValues.LangResource), StatusCodes.Status400BadRequest);
            }
            var langResourceResult = _mapper.Map<LangResourceDto>(langResourceExists);
            return new SuccessDataResult<LangResourceDto>(langResourceResult);
        }
        #endregion
        #region UpdateAsync
        public async Task<DataResult<LangResourceDto>> UpdateAsync(LangResourceUpdateDto model)
        {
            var messageCodeExists = await _efLangResourceRepository.GetAsync(x => x.Id != model.Id && x.MessageCode == model.MessageCode);
            if (messageCodeExists != null)
            {
                return new ErrorDataResult<LangResourceDto>(MessageExtensions.AlreadyExists(LangResourceConstantValues.LangResourceMessageCode), StatusCodes.Status400BadRequest);
            }
            var langResourceUpdated = _mapper.Map<LangResource>(model);
            await _efLangResourceRepository.UpdateAsync(langResourceUpdated);
            var langResourceResult = _mapper.Map<LangResourceDto>(langResourceUpdated);
            return new SuccessDataResult<LangResourceDto>(langResourceResult);
        }
        #endregion
        #region DeleteAsync
        public async Task<IResult> DeleteAsync(int id)
        {
            var langResourceExists = await _efLangResourceRepository.GetAsync(x => x.Id == id);
            if (langResourceExists == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(LangResourceConstantValues.LangResource));
            }
            await _efLangResourceRepository.DeleteAsync(langResourceExists);
            var langResourceDeleted = await _efLangResourceRepository.GetAsync(x => x.Id == langResourceExists.Id);
            if (langResourceDeleted == null)
            {
                return new SuccessResult(MessageExtensions.Deleted(LangResourceConstantValues.LangResource));
            }
            return new ErrorResult(MessageExtensions.NotDeleted(LangResourceConstantValues.LangResource));
        }
        #endregion
        #region GetAllAsync
        public async Task<DataResult<List<LangResourceDto>>> GetAllAsync()
        {
            var langResources = await _dapperLangResourceRepository.GetAllAsync();
            if (langResources == null || langResources!.Count < 1)
            {
                return new ErrorDataResult<List<LangResourceDto>>(MessageExtensions.NotFound(LangResourceConstantValues.LangResource));
            }
            var langResourceResult = _mapper.Map<List<LangResourceDto>>(langResources);
            return new SuccessDataResult<List<LangResourceDto>>(langResourceResult);
        }
        #endregion
        #region GetAllByLangIdAsync
        public async Task<DataResult<List<LangResourceDto>>> GetAllByLangIdAsync(int langId)
        {
            var langResources = await _dapperLangResourceRepository.GetAllByLangIdAsync(langId);
            if (langResources == null || langResources!.Count < 1)
            {
                return new ErrorDataResult<List<LangResourceDto>>(MessageExtensions.NotFound(LangResourceConstantValues.LangResource));
            }
            var langResourceResult = _mapper.Map<List<LangResourceDto>>(langResources);
            return new SuccessDataResult<List<LangResourceDto>>(langResourceResult);
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<DataResult<List<LangResourceDto>>> GetAllPagingAsync(PagingDto model)
        {
            var langResources = await _dapperLangResourceRepository.GetAllPagingAsync(model);
            if (langResources == null || langResources!.Count < 1)
            {
                return new ErrorDataResult<List<LangResourceDto>>(MessageExtensions.NotFound(LangResourceConstantValues.LangResource));
            }
            var langResourceResult = _mapper.Map<List<LangResourceDto>>(langResources);
            return new SuccessDataResult<List<LangResourceDto>>(langResourceResult);
        }
        #endregion
        #region GetByMessageCodeAsync
        public async Task<DataResult<LangResourceDto>> GetByMessageCodeAsync(string messageCode)
        {
            var langResource = await _dapperLangResourceRepository.GetByMessageCodeAsync(messageCode);
            if (langResource == null)
            {
                return new ErrorDataResult<LangResourceDto>(MessageExtensions.NotFound(LangResourceConstantValues.LangResource));
            }
            var langResourceResult = _mapper.Map<LangResourceDto>(langResource);
            return new SuccessDataResult<LangResourceDto>(langResourceResult);
        }
        #endregion

    }
}
