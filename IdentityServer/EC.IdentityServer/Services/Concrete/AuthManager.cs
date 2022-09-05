using AutoMapper;
using EC.IdentityServer.Constants;
using EC.IdentityServer.Dtos;
using EC.IdentityServer.Models.Identity;
using EC.IdentityServer.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Shared.Utilities.Messages;
using Shared.Utilities.Results;
using IResult = Shared.Utilities.Results.IResult;

namespace EC.IdentityServer.Services.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AuthManager(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IResult> RegisterAsync(RegisterDto model)
        {
            var userPhoneNumberExists = await _userManager.FindByNameAsync(model.PhoneNumber);
            var userEmailExists = await _userManager.FindByEmailAsync(model.PhoneNumber);
            if (userPhoneNumberExists != null)
            {
                return new ErrorResult(ErrorMessages.AlreadyExists(PropertyNames.PhoneNumber));
            }
            if (userEmailExists != null)
            {
                return new ErrorResult(ErrorMessages.AlreadyExists(PropertyNames.Email));
            }
            var userAdded = _mapper.Map<AppUser>(model);
            //BURADA PHONENUMBER CHECK YAPILACAK...
            userAdded.UserName = model.PhoneNumber;
            var result = await _userManager.CreateAsync(userAdded, model.Password);
            if (result.Succeeded)
            {
                string userRole = "User.Normal";
                var roleResult = _userManager.AddToRoleAsync(userAdded, userRole);
            }
        }
    }
}
