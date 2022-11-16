using Core.Dtos;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.LangResourceAPI.Constants;
using EC.Services.LangResourceAPI.Dtos.LangDtos;
using EC.Services.LangResourceAPI.Dtos.LangResourceDtos;
using EC.Services.LangResourceAPI.Services.Abstract;
using EC.Services.LangResourceAPI.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Core.Utilities.IoC;
using Core.CrossCuttingConcerns.Caching.Redis;

namespace EC.Services.LangResourceAPI.Controllers
{
    [Route("langresource/api/[controller]")]
    [ApiController]
    public class LangResourcesController : ControllerBase
    {
        private readonly ILangResourceService _langResourceService;
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly IConfiguration _configuration;

        public LangResourcesController(ILangResourceService langResourceService,IRedisCacheManager redisCacheManager)
        {
            _langResourceService = langResourceService;
            _redisCacheManager = redisCacheManager;
            _configuration = ServiceTool.ServiceProvider.GetRequiredService<IConfiguration>();
        }

        #region RefreshAsync
        [HttpGet]
        [Route("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {
            var result = await _langResourceService.GetAllAsync();
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }

            JsonSerializerOptions serializeOptions = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            var redisDbId = _configuration.GetValue<int>("LangResourceRedisDbId");
            var status = _redisCacheManager.GetDatabase(db: redisDbId).StringSet("langResources", JsonSerializer.Serialize(result.Data, serializeOptions));
            if (!status)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDataResult<List<LangDto>>(MessageExtensions.NotCreated(LangResourceConstantValues.LangResourceLang), StatusCodes.Status500InternalServerError));
            }
            return StatusCode(StatusCodes.Status200OK, new SuccessDataResult<List<LangDto>>());
        }
        #endregion
        #region AddAsync
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync(LangResourceAddDto model)
        {
            var result = await _langResourceService.AddAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region UpdateAsync
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync(LangResourceUpdateDto model)
        {
            var result = await _langResourceService.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync(DeleteIntDto model)
        {
            var result = await _langResourceService.DeleteAsync(model.Id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _langResourceService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllByLangIdAsync
        [HttpGet]
        [Route("getall-by-langid")]
        public async Task<IActionResult> GetAllByLangIdAsync(int langId)
        {
            var result = await _langResourceService.GetAllByLangIdAsync(langId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllPagingAsync
        [HttpGet]
        [Route("getall-paging")]
        public async Task<IActionResult> GetAllPagingAsync(PagingDto model)
        {
            var result = await _langResourceService.GetAllPagingAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetByMessageCodeAsync
        [HttpGet]
        [Route("get-by-messagecode")]
        public async Task<IActionResult> GetByMessageCodeAsync(string messageCode)
        {
            var result = await _langResourceService.GetByMessageCodeAsync(messageCode);
            return StatusCode(result.StatusCode, result);
        }
        #endregion

    }
}
