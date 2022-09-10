using Core.Extensions;
using Core.Utilities.Results;
using EC.IdentityServer.Constants;
using EC.IdentityServer.Dtos;
using EC.IdentityServer.Extensions;
using EC.IdentityServer.Helpers;
using EC.IdentityServer.Models.Identity;
using EC.IdentityServer.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;

namespace EC.IdentityServer.Controllers
{
    [Route("identity/api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public UsersController(IAuthService authService,IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        #region Register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var result = await _authService.RegisterAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region SendActivationCodeAsync
        [HttpPost]
        [Route("send-activation-code")]
        public async Task<IActionResult> SendActivationCodeAsync()
        {
            string token = Request.Headers[HeaderNames.Authorization];
            var userId = TokenExtensions.GetTokenTypeValue(token,"sub");

            if (userId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,new ErrorResult(MessageExtensions.NotFound(PropertyNames.User)));
            }
            var result = await _userService.ActivateAccountSendSms(userId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion


    }
}
