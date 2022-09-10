using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #region Create
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(ProductAddDto model)
        {
           var result= await _productRepository.CreateAsync(model);
            return StatusCode(result.StatusCode,result);
        }
        #endregion
        #region Update
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(ProductUpdateDto model)
        {
            var result = await _productRepository.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion

    }
}
