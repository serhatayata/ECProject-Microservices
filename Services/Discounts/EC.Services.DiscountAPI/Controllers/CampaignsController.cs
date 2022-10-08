using Core.Dtos;
using Core.Utilities.Attributes;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.DiscountAPI.Controllers
{
    [Route("campaign/api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignsController(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }


        #region AddAsync
        [HttpPost]
        [Route("add")]
        [AuthorizeAnyPolicy("WriteDiscount,FullDiscount")]
        public async Task<IActionResult> AddAsync([FromBody] CampaignAddDto model)
        {
            var result = await _campaignRepository.CreateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region UpdateAsync
        [HttpPut]
        [Route("update")]
        [AuthorizeAnyPolicy("WriteDiscount,FullDiscount")]
        public async Task<IActionResult> UpdateAsync([FromBody] CampaignUpdateDto model)
        {
            var result = await _campaignRepository.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        [AuthorizeAnyPolicy("WriteDiscount,FullDiscount")]
        public async Task<IActionResult> DeleteAsync([FromBody]DeleteStringDto model)
        {
            var result = await _campaignRepository.DeleteAsync(model.Id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteProductAsync
        [HttpDelete]
        [Route("delete-product")]
        [AuthorizeAnyPolicy("WriteDiscount,FullDiscount")]
        public async Task<IActionResult> DeleteProductAsync([FromBody] CampaignDeleteProductDto model)
        {
            var result = await _campaignRepository.DeleteProductAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region AddProductsAsync
        [HttpPost]
        [Route("add-product")]
        [AuthorizeAnyPolicy("WriteDiscount,FullDiscount")]
        public async Task<IActionResult> AddProductsAsync([FromBody]CampaignAddProductsDto model)
        {
            var result = await _campaignRepository.AddProductsAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAsync
        [HttpGet]
        [Route("get")]
        [AuthorizeAnyPolicy("ReadDiscount,FullDiscount")]
        public async Task<IActionResult> GetAsync([FromQuery] CampaignGetByIdDto model)
        {
            var result = await _campaignRepository.GetAsync(model.Id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("getall")]
        [AuthorizeAnyPolicy("ReadDiscount,FullDiscount")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _campaignRepository.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetProductCampaignsAsync
        [HttpGet]
        [Route("get-byproductid")]
        [AuthorizeAnyPolicy("ReadDiscount,FullDiscount")]
        public async Task<IActionResult> GetProductCampaignsAsync([FromQuery] string productId)
        {
            var result = await _campaignRepository.GetProductCampaignsAsync(productId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
