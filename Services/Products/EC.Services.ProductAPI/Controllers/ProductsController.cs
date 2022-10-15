using Core.Dtos;
using Core.Utilities.Attributes;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
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
        [AuthorizeAnyPolicy("WriteProduct,FullProduct")]
        public async Task<IActionResult> CreateAsync(ProductAddDto model)
        {
           var result= await _productRepository.CreateAsync(model);
            return StatusCode(result.StatusCode,result);
        }
        #endregion
        #region UpdateAsync
        [HttpPut]
        [Route("update")]
        [AuthorizeAnyPolicy("WriteProduct,FullProduct")]
        public async Task<IActionResult> UpdateAsync(ProductUpdateDto model)
        {
            var result = await _productRepository.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        [AuthorizeAnyPolicy("WriteProduct,FullProduct")]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteStringDto model)
        {
            var result = await _productRepository.DeleteAsync(model.Id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAsync
        [HttpGet]
        [Route("get")]
        [AuthorizeAnyPolicy("ReadProduct,FullProduct")]
        public async Task<IActionResult> GetAsync([FromQuery]ProductGetByIdDto model)
        {
            var result = await _productRepository.GetAsync(model.Id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("getall")]
        [AuthorizeAnyPolicy("ReadProduct,FullProduct")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _productRepository.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllPagingAsync
        [HttpGet]
        [Route("getall-paging")]
        [AuthorizeAnyPolicy("ReadProduct,FullProduct")]
        public async Task<IActionResult> GetAllPagingAsync([FromQuery]PagingDto model)
        {
            var result = await _productRepository.GetAllPagingAsync(model.Page,model.PageSize);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetProductByCategoryIdAsync
        [HttpGet]
        [Route("get-products-by-category-id")]
        [AuthorizeAnyPolicy("ReadProduct,FullProduct")]
        public async Task<IActionResult> GetProductByCategoryIdAsync([FromQuery]int categoryId)
        {
            var result = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetProductByNameAsync
        [HttpGet]
        [Route("get-products-by-name")]
        [AuthorizeAnyPolicy("ReadProduct,FullProduct")]
        public async Task<IActionResult> GetProductsByNameAsync([FromQuery]string name)
        {
            var result = await _productRepository.GetProductsByNameAsync(name);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetProductsByProductIdsAsync
        [HttpPost]
        [Route("get-products-by-ids")]
        [AuthorizeAnyPolicy("ReadProduct,FullProduct")]
        public async Task<IActionResult> GetProductsByProductIdsAsync([FromBody] ProductGetProductsByIdsDto model)
        {
            var result = await _productRepository.GetProductsByProductIds(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion

        
    }
}
