using Core.Utilities.Attributes;
using Core.Utilities.Business.Abstract;
using EC.Services.BasketAPI.Dtos;
using EC.Services.BasketAPI.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EC.Services.BasketAPI.Controllers
{
    [Route("basket/api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;
        string _userId;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
            _userId = _sharedIdentityService.GetUserId;
        }

        #region GetBasket
        [HttpGet]
        [Route("get")]
        [AuthorizeAnyPolicy("FullBasket,ReadBasket")]
        public async Task<IActionResult> GetBasket()
        {
            var result = await _basketService.GetBasket(_userId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region SaveOrUpdateBasket
        [HttpPost]
        [Route("save-or-update")]
        [AuthorizeAnyPolicy("FullBasket,WriteBasket")]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketSaveOrUpdateDto basketDto)
        {
            var result = await _basketService.SaveOrUpdate(basketDto);
            return StatusCode(result.StatusCode,result);
        }
        #endregion
        #region DeleteBasket
        [HttpDelete]
        [Route("delete")]
        [AuthorizeAnyPolicy("FullBasket,WriteBasket")]
        public async Task<IActionResult> DeleteBasket()
        {
            var result = await _basketService.Delete(_userId);
            return StatusCode(result.StatusCode,result);
        }
        #endregion

    }
}
