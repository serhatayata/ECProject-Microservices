using Core.Dtos;
using Core.Utilities.Attributes;
using EC.Services.DiscountAPI.Dtos.Discount;
using EC.Services.DiscountAPI.Entities;
using EC.Services.DiscountAPI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MongoDB.Libmongocrypt.CryptContext;

namespace EC.Services.DiscountAPI.Controllers
{
    [Route("discount/api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        #region CreateAsync
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody]DiscountAddDto model)
        {
            var result = await _discountService.CreateAsync(model);
            return StatusCode(result.StatusCode,result);
        }
        #endregion
        #region UpdateAsync
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync([FromBody]DiscountUpdateDto model)
        {
            var result = await _discountService.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery]DeleteIntDto model)
        {
            var result = await _discountService.DeleteAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region ActivateDiscountAsync
        [HttpPost]
        [Route("activate-discount")]
        public async Task<IActionResult> ActivateDiscountAsync([FromBody] DiscountIdDto model)
        {
            var result = await _discountService.ActivateDiscountAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetWithStatusByIdAsync
        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> GetWithStatusByIdAsync([FromQuery] DiscountIdDto model)
        {
            var result = await _discountService.GetWithStatusByIdAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllWithStatusAsync
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllWithStatusAsync([FromQuery] DiscountStatus status = DiscountStatus.Active)
        {
            var result = await _discountService.GetAllWithStatusAsync(status);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetDiscountByCodeAsync
        [HttpGet]
        [Route("get-by-code")]
        public async Task<IActionResult> GetDiscountByCodeAsync([FromQuery] DiscountGetByCodeDto model)
        {
            var result = await _discountService.GetDiscountByCodeAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
