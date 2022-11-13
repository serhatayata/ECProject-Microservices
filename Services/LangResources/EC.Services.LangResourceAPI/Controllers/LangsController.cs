using Core.Dtos;
using EC.Services.LangResourceAPI.Dtos.LangDtos;
using EC.Services.LangResourceAPI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.LangResourceAPI.Controllers
{
    [Route("langresource/api/[controller]")]
    [ApiController]
    public class LangsController : ControllerBase
    {
        private readonly ILangService _langService;

        public LangsController(ILangService langService)
        {
            _langService = langService;
        }

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
