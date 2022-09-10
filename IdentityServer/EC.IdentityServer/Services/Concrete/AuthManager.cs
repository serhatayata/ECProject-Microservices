using AutoMapper;
using Core.Aspects.Autofac.Transaction;
using Core.Extensions;
using Core.Utilities.Messages;
using Core.Utilities.Results;
using EC.IdentityServer.Constants;
using EC.IdentityServer.Dtos;
using EC.IdentityServer.Models.Identity;
using EC.IdentityServer.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using IResult = Core.Utilities.Results.IResult;

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

        #region RegisterAsync
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> RegisterAsync(RegisterDto model)
        {
            var userPhoneNumberExists = await _userManager.FindByNameAsync(model.PhoneNumber);
            var userEmailExists = await _userManager.FindByEmailAsync(model.PhoneNumber);

            if (userPhoneNumberExists != null)
            {
                return new ErrorResult(MessageExtensions.AlreadyExists(PropertyNames.PhoneNumber));
            }
            if (userEmailExists != null)
            {
                return new ErrorResult(MessageExtensions.AlreadyExists(PropertyNames.Email));
            }

            var userAdded = _mapper.Map<AppUser>(model);
            //BURADA PHONENUMBER CHECK YAPILACAK...
            userAdded.UserName = model.PhoneNumber;
            var result = await _userManager.CreateAsync(userAdded, model.Password);
            if (result.Succeeded)
            {
                string userRole = "User.Normal";
                var roleResult = await _userManager.AddToRoleAsync(userAdded, userRole);
                return new SuccessResult(MessageExtensions.Added(PropertyNames.User));
            }
            return new ErrorResult(MessageExtensions.NotAdded(PropertyNames.User));
        }
        #endregion

    }
}
