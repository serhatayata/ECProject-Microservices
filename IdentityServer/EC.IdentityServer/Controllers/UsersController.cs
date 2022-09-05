using EC.IdentityServer.Dtos;
using EC.IdentityServer.Models.Identity;
using EC.IdentityServer.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IdentityServer4.IdentityServerConstants;

namespace EC.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var result = await _authService.RegisterAsync(model);
            return StatusCode(result.StatusCode, result);
        }


    }
}
