using EC.Services.ProductAPI.Dtos.BaseDtos;
using EC.Services.ProductAPI.Dtos.VariantDtos;
using EC.Services.ProductAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.ProductAPI.Controllers
{
    [Route("product/api/[controller]")]
    [ApiController]
    public class VariantsController : ControllerBase
    {
        private readonly IVariantRepository _variantRepository;

        public VariantsController(IVariantRepository variantRepository)
        {
            _variantRepository = variantRepository;
        }

        #region CreateAsync
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync(VariantAddDto model)
        {
            var result = await _variantRepository.CreateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region UpdateAsync
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync(VariantUpdateDto model)
        {
            var result = await _variantRepository.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _variantRepository.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAsync
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAsync([FromQuery] string id)
        {
            var result = await _variantRepository.GetAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _variantRepository.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("getall-paging")]
        public async Task<IActionResult> GetAllPagingAsync([FromQuery] PagingDto model)
        {
            var result = await _variantRepository.GetAllPagingAsync(model.Page, model.PageSize);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
