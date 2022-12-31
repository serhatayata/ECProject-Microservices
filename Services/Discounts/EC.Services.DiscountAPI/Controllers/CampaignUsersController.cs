using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignUser;
using EC.Services.DiscountAPI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.DiscountAPI.Controllers
{
    [Route("discount/api/[controller]")]
    [ApiController]
    public class CampaignUsersController : ControllerBase
    {
        private readonly ICampaignUserService _campaignUserService;

        public CampaignUsersController(ICampaignUserService campaignUserService)
        {
            _campaignUserService = campaignUserService;
        }

        #region AddAsync
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody]CampaignUserAddDto model)
        {
            var result = await _campaignUserService.AddAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] CampaignUserDeleteDto model)
        {
            var result = await _campaignUserService.DeleteAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllByCampaignIdAsync
        [HttpGet]
        [Route("getall-by-campaignid")]
        public async Task<IActionResult> GetAllByCampaignIdAsync([FromQuery] CampaignIdDto model)
        {
            var result = await _campaignUserService.GetAllByCampaignIdAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllByCampaignIdPagingAsync
        [HttpGet]
        [Route("getall-by-campaignid-paging")]
        public async Task<IActionResult> GetAllByCampaignIdPagingAsync([FromQuery] CampaignIdPagingDto model)
        {
            var result = await _campaignUserService.GetAllByCampaignIdPagingAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllByUserIdAsync
        [HttpGet]
        [Route("getall-by-userid")]
        public async Task<IActionResult> GetAllByUserIdAsync([FromQuery] CampaignUserIdDto model)
        {
            var result = await _campaignUserService.GetAllByUserIdAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
