using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Dtos.ProductVariantDtos;
using EC.Services.ProductAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.ProductAPI.Controllers
{
    [Route("product/api/[controller]")]
    [ApiController]
    public class ProductVariantsController : ControllerBase
    {
        private readonly IProductVariantRepository _productVariantRepository;

        public ProductVariantsController(IProductVariantRepository productVariantRepository)
        {
            _productVariantRepository = productVariantRepository;
        }

        #region CreateAsync
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync(ProductVariantDto model)
        {
            var result = await _productVariantRepository.CreateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync(ProductVariantDeleteDto model)
        {
            var result = await _productVariantRepository.DeleteByProductAndVariantId(model.ProductId,model.VariantId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAsync
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAsync([FromQuery] ProductVariantGetDto model)
        {
            var result = await _productVariantRepository.GetByProductAndVariantId(model.ProductId,model.VariantId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _productVariantRepository.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion

    }
}
