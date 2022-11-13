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
        public async Task<IActionResult> AddAsync(LangResourceAddDto model)
        {
            var result = await _langResourceService.AddAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region UpdateAsync
        public async Task<IActionResult> UpdateAsync(LangResourceUpdateDto model)
        {
            var result = await _langResourceService.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _langResourceService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _langResourceService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllByLangIdAsync
        public async Task<IActionResult> GetAllByLangIdAsync(int langId)
        {
            var result = await _langResourceService.GetAllByLangIdAsync(langId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<IActionResult> GetAllPagingAsync(PagingDto model)
        {
            var result = await _langResourceService.GetAllPagingAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetByMessageCodeAsync
        public async Task<IActionResult> GetByMessageCodeAsync(string messageCode)
        {
            var result = await _langResourceService.GetByMessageCodeAsync(messageCode);
            return StatusCode(result.StatusCode, result);
        }
        #endregion

    }
}
