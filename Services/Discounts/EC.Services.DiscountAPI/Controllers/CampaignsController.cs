using Core.Dtos;
using Core.Utilities.Attributes;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignProduct;
using EC.Services.DiscountAPI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.DiscountAPI.Controllers
{
    [Route("discount/api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        #region GetAsync
        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> GetByIdAsync(CampaignGetWithStatusDto model)
        {
            var result = await _campaignService.GetWithStatusByIdAsync(model.Id, model.Status);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAsync(CampaignGetAllWithStatusDto model)
        {
            var result = await _campaignService.GetAllWithStatusAsync(model.Status);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetProductCampaignsAsync
        [HttpGet]
        [Route("get-product-campaigns")]
        public async Task<IActionResult> GetProductCampaignsAsync(int productId)
        {
            var result = await _campaignService.GetProductCampaignsAsync(productId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region CreateAsync
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync(CampaignAddDto model)
        {
            var result = await _campaignService.CreateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region AddProductAsync
        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddProductAsync(CampaignAddProductDto model)
        {
            var result = await _campaignService.AddProductAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region UpdateAsync
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync(CampaignUpdateDto model)
        {
            var result = await _campaignService.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync(DeleteIntDto model)
        {
            var result = await _campaignService.DeleteAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteProductAsync
        [HttpDelete]
        [Route("delete-product")]
        public async Task<IActionResult> DeleteProductAsync(CampaignDeleteProductDto model)
        {
            var result = await _campaignService.DeleteProductAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAllProductsByCampaignIdAsync
        [HttpDelete]
        [Route("delete-all-products")]
        public async Task<IActionResult> DeleteAllProductsByCampaignIdAsync(DeleteIntDto model)
        {
            var result = await _campaignService.DeleteAllProductsByCampaignIdAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region ActivateCampaignAsync
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> ActivateCampaignAsync(DeleteIntDto model)
        {
            var result = await _campaignService.ActivateCampaignAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
