using Core.Dtos;
using Core.Utilities.Attributes;
using EC.Services.DiscountAPI.Dtos.Discount;
using EC.Services.DiscountAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.DiscountAPI.Controllers
{
    [Route("discount/api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountsController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        #region AddAsync
        [HttpPost]
        [Route("add")]
        [AuthorizeAnyPolicy("WriteDiscount,FullDiscount")]
        public async Task<IActionResult> AddAsync([FromBody] DiscountAddDto model)
        {
            var result = await _discountRepository.CreateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region UpdateAsync
        [HttpPut]
        [Route("update")]
        [AuthorizeAnyPolicy("WriteDiscount,FullDiscount")]
        public async Task<IActionResult> UpdateAsync([FromBody] DiscountUpdateDto model)
        {
            var result = await _discountRepository.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        [AuthorizeAnyPolicy("WriteDiscount,FullDiscount")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteStringDto model)
        {
            var result = await _discountRepository.DeleteAsync(model.Id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAsync
        [HttpGet]
        [Route("get")]
        [AuthorizeAnyPolicy("ReadDiscount,FullDiscount")]
        public async Task<IActionResult> GetAsync([FromQuery] DiscountGetByIdDto model)
        {
            var result = await _discountRepository.GetAsync(model.Id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("getall")]
        [AuthorizeAnyPolicy("ReadDiscount,FullDiscount")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _discountRepository.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetDiscountByCodeAsync
        [HttpGet]
        [Route("get-discount-bycode")]
        [AuthorizeAnyPolicy("ReadDiscount,FullDiscount")]
        public async Task<IActionResult> GetDiscountByCodeAsync([FromQuery]DiscountGetByCodeDto model)
        {
            var result = await _discountRepository.GetDiscountByCodeAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion

    }
}
