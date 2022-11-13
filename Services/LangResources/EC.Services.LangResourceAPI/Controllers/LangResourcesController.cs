using Core.Dtos;
using EC.Services.LangResourceAPI.Dtos.LangResourceDtos;
using EC.Services.LangResourceAPI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
