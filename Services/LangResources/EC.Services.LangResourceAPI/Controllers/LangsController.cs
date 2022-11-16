using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using EC.Services.LangResourceAPI.Constants;
using EC.Services.LangResourceAPI.Dtos.LangDtos;
using EC.Services.LangResourceAPI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EC.Services.LangResourceAPI.Controllers
{
    [Route("langresource/api/[controller]")]
    [ApiController]
    public class LangsController : ControllerBase
    {
        private readonly ILangService _langService;
        private readonly ILangResourceService _langResourceService;
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly IConfiguration _configuration;

        public LangsController(ILangService langService, ILangResourceService langResourceService, IRedisCacheManager redisCacheManager)
        {
            _langService = langService;
            _langResourceService = langResourceService;
            _redisCacheManager = redisCacheManager;
            _configuration = ServiceTool.ServiceProvider.GetRequiredService<IConfiguration>();
        }

        #region RefreshAsync
        [HttpGet]
        [Route("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {
            var result = await _langService.GetAllAsync();
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }

            JsonSerializerOptions serializeOptions = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var redisDbId = _configuration.GetValue<int>("LangResourceRedisDbId");
            var status = _redisCacheManager.GetDatabase(db: redisDbId).StringSet("langs", JsonSerializer.Serialize(result.Data, serializeOptions));
            if (!status)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDataResult<List<LangDto>>(MessageExtensions.NotCreated(LangResourceConstantValues.LangResourceLang), StatusCodes.Status500InternalServerError));
            }
            return StatusCode(StatusCodes.Status200OK,new SuccessDataResult<List<LangDto>>());
        }
        #endregion
        #region AddAsync
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody] LangAddDto model)
        {
            var result = await _langService.AddAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region UpdateAsync
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] LangUpdateDto model)
        {
            var result = await _langService.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteIntDto model)
        {
            var result = await _langService.DeleteAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _langService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllPaging
        [HttpGet]
        [Route("getall-paging")]
        public async Task<IActionResult> GetAllPagingAsync([FromQuery] PagingDto model)
        {
            var result = await _langService.GetAllPagingAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetByCode
        [HttpGet]
        [Route("get-by-code")]
        public async Task<IActionResult> GetByCodeAsync([FromQuery]string code)
        {
            var result = await _langService.GetByCodeAsync(code);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
