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
        public async Task<IActionResult> DeleteAsync([FromBody] string id)
        {
            var result = await _discountRepository.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAsync
        [HttpPost]
        [Route("get")]
        [AuthorizeAnyPolicy("ReadDiscount,FullDiscount")]
        public async Task<IActionResult> GetAsync([FromBody] string id)
        {
            var result = await _discountRepository.GetAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpPost]
        [Route("getall")]
        [AuthorizeAnyPolicy("ReadDiscount,FullDiscount")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _discountRepository.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetDiscountByCodeAsync
        [HttpPost]
        [Route("get-discount-bycode")]
        [AuthorizeAnyPolicy("ReadDiscount,FullDiscount")]
        public async Task<IActionResult> GetDiscountByCodeAsync([FromBody]string code)
        {
            var result = await _discountRepository.GetDiscountByCodeAsync(code);
            return StatusCode(result.StatusCode, result);
        }
        #endregion

    }
}
