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


    }
}
