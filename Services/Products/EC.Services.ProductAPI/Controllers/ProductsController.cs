using EC.Services.ProductAPI.Dtos.BaseDtos;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.ProductAPI.Controllers
{
    [Route("product/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #region CreateAsync
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync(ProductAddDto model)
        {
           var result= await _productRepository.CreateAsync(model);
            return StatusCode(result.StatusCode,result);
        }
        #endregion
        #region UpdateAsync
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync(ProductUpdateDto model)
        {
            var result = await _productRepository.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _productRepository.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAsync
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAsync([FromQuery]string id)
        {
            var result = await _productRepository.GetAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _productRepository.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllPagingAsync
        [HttpGet]
        [Route("getall-paging")]
        public async Task<IActionResult> GetAllPagingAsync([FromQuery]PagingDto model)
        {
            var result = await _productRepository.GetAllPagingAsync(model.Page,model.PageSize);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetProductByCategoryIdAsync
        [HttpGet]
        [Route("get-products-by-category-id")]
        public async Task<IActionResult> GetProductByCategoryIdAsync([FromQuery]int categoryId)
        {
            var result = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetProductByNameAsync
        [HttpGet]
        [Route("get-products-by-name")]
        public async Task<IActionResult> GetProductsByNameAsync([FromQuery]string name)
        {
            var result = await _productRepository.GetProductsByNameAsync(name);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
