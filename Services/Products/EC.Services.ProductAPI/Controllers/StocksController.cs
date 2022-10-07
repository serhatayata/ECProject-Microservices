using Core.Dtos;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Dtos.StockDtos;
using EC.Services.ProductAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.ProductAPI.Controllers
{
    [Route("product/api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StocksController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        #region CreateAsync
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync(StockAddDto model)
        {
            var result = await _stockRepository.CreateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region UpdateAsync
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync(StockUpdateDto model)
        {
            var result = await _stockRepository.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _stockRepository.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAsync
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAsync([FromQuery] StockGetByIdDto model)
        {
            var result = await _stockRepository.GetAsync(model.Id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetByProductIdAsync
        [HttpGet]
        [Route("get-by-productid")]
        public async Task<IActionResult> GetByProductIdAsync([FromQuery] string productId)
        {
            var result = await _stockRepository.GetByProductIdAsync(productId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _stockRepository.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllPagingAsync
        [HttpGet]
        [Route("getall-paging")]
        public async Task<IActionResult> GetAllPagingAsync([FromQuery] PagingDto model)
        {
            var result = await _stockRepository.GetAllPagingAsync(model.Page, model.PageSize);
            return StatusCode(result.StatusCode, result);
        }
        #endregion


    }
}
