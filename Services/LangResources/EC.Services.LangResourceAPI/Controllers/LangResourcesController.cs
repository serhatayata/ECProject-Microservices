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

        public LangResourcesController(ILangResourceService langResourceService)
        {
            _langResourceService = langResourceService;
        }

        #region RefreshAsync
        [HttpGet]
        [Route("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {
            var result = await _langResourceService.RefreshAsync();
            return StatusCode(result.StatusCode,result);
        }
        #endregion
        #region AddAsync
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody]LangResourceAddDto model)
        {
            var result = await _langResourceService.AddAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region UpdateAsync
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] LangResourceUpdateDto model)
        {
            var result = await _langResourceService.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteIntDto model)
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
        public async Task<IActionResult> GetAllByLangIdAsync([FromQuery]int langId)
        {
            var result = await _langResourceService.GetAllByLangIdAsync(langId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllPagingAsync
        [HttpGet]
        [Route("getall-paging")]
        public async Task<IActionResult> GetAllPagingAsync([FromQuery]PagingDto model)
        {
            var result = await _langResourceService.GetAllPagingAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetByMessageCodeAsync
        [HttpGet]
        [Route("get-by-messagecode")]
        public async Task<IActionResult> GetByMessageCodeAsync([FromQuery] string messageCode)
        {
            var result = await _langResourceService.GetByMessageCodeAsync(messageCode);
            return StatusCode(result.StatusCode, result);
        }
        #endregion

    }
}
