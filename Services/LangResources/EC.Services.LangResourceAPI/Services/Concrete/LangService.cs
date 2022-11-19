using AutoMapper;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using EC.Services.LangResourceAPI.Constants;
using EC.Services.LangResourceAPI.Data.Abstract.Dapper;
using EC.Services.LangResourceAPI.Data.Abstract.EntityFramework;
using EC.Services.LangResourceAPI.Dtos.LangDtos;
using EC.Services.LangResourceAPI.Entities;
using EC.Services.LangResourceAPI.Services.Abstract;
using System.Text.Json;
using System.Text.Json.Serialization;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.LangResourceAPI.Services.Concrete
{
    public class LangService : ILangService
    {
        private readonly IEfLangRepository _efLangRepository;
        private readonly IDapperLangRepository _dapperLangRepository;
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public LangService(IEfLangRepository efLangRepository,IRedisCacheManager redisCacheManager, IDapperLangRepository dapperLangRepository, IMapper mapper)
        {
            _efLangRepository = efLangRepository;
            _dapperLangRepository = dapperLangRepository;
            _redisCacheManager = redisCacheManager;
            _configuration = ServiceTool.ServiceProvider.GetRequiredService<IConfiguration>();
            _mapper = mapper;
        }

        #region RefreshAsync
        public async Task<IResult> RefreshAsync()
        {
            var result = await _dapperLangRepository.GetAllAsync();
            if (result == null || result?.Count < 1)
            {
                return new ErrorResult(MessageExtensions.NotRefreshed(LangResourceConstantValues.LangResourceRedis));
            }

            JsonSerializerOptions serializeOptions = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var redisDbId = _configuration.GetValue<int>("LangResourceRedisDbId");
            var status = _redisCacheManager.GetDatabase(db: redisDbId).StringSet("langs", JsonSerializer.Serialize(result, serializeOptions));
            if (!status)
            {
                return new ErrorResult(MessageExtensions.NotRefreshed(LangResourceConstantValues.LangResourceRedis));
            }
            return new SuccessResult(MessageExtensions.Refreshed(LangResourceConstantValues.LangResourceRedis));
        }
        #endregion
        #region AddAsync
        public async Task<DataResult<LangDto>> AddAsync(LangAddDto model)
        {
            var codeExists = await _efLangRepository.GetAsync(x => x.Code == model.Code);
            if (codeExists != null)
            {
                return new ErrorDataResult<LangDto>(MessageExtensions.AlreadyExists(LangResourceConstantValues.LangResourceLangCode));
            }
            var displayNameExists = await _efLangRepository.GetAsync(x => x.DisplayName == model.DisplayName);
            if (displayNameExists != null)
            {
                return new ErrorDataResult<LangDto>(MessageExtensions.AlreadyExists(LangResourceConstantValues.LangResourceLangDisplayName));
            }
            var langAdded = _mapper.Map<Lang>(model);
            await _efLangRepository.AddAsync(langAdded);
            var langExists = await _efLangRepository.GetAsync(x => x.Id == langAdded.Id);
            if (langExists == null)
            {
                return new ErrorDataResult<LangDto>(MessageExtensions.NotAdded(LangResourceConstantValues.LangResourceLang));
            }
            var langResult = _mapper.Map<LangDto>(langExists);
            await this.RefreshAsync();

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
            var displayNameExists = await _efLangRepository.GetAsync(x => x.Id != model.Id && x.DisplayName == model.DisplayName);
            if (displayNameExists != null)
            {
                return new ErrorDataResult<LangDto>(MessageExtensions.AlreadyExists(LangResourceConstantValues.LangResourceLangDisplayName));
            }
            var langUpdated = _mapper.Map<Lang>(model);
            await _efLangRepository.UpdateAsync(langUpdated);

            var langResult = _mapper.Map<LangDto>(langUpdated);
            await this.RefreshAsync();

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
                await this.RefreshAsync();
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
        #region GetByDisplayNameAsync
        public async Task<DataResult<LangDto>> GetByDisplayNameAsync(string displayName)
        {
            var lang = await _dapperLangRepository.GetByDisplayNameAsync(displayName);
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
