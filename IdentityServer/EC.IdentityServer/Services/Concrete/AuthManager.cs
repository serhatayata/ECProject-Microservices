using AutoMapper;
using Core.Aspects.Autofac.Transaction;
using Core.Extensions;
using Core.Utilities.Messages;
using Core.Utilities.Results;
using EC.IdentityServer.ApiServices.Abstract;
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
        private readonly ICommunicationApiService _commApiService;
        private readonly IMapper _mapper;

        public AuthManager(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, ICommunicationApiService commApiService, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _commApiService = commApiService;
            _mapper = mapper;
        }

        #region RegisterAsync
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> RegisterAsync(RegisterDto model)
        {
            var userPhoneNumberExists = await _userManager.FindByNameAsync(model.PhoneNumber);
            if (userPhoneNumberExists != null)
            {
                return new ErrorResult(MessageExtensions.AlreadyExists(PropertyNames.PhoneNumber));
            }

            var userEmailExists = await _userManager.FindByEmailAsync(model.PhoneNumber);
            if (userEmailExists != null)
            {
                return new ErrorResult(MessageExtensions.AlreadyExists(PropertyNames.Email));
            }
            
            //Send communication api service activation code, before that send it to redis cache.......


            //var userAdded = _mapper.Map<AppUser>(model);
            //userAdded.UserName = model.PhoneNumber;
            //var result = await _userManager.CreateAsync(userAdded, model.Password);
            //if (result.Succeeded)
            //{
            //    string userRole = "User.Normal";
            //    var roleResult = await _userManager.AddToRoleAsync(userAdded, userRole);
            //    return new SuccessResult(MessageExtensions.Added(PropertyNames.User));
            //}
            //return new ErrorResult(MessageExtensions.NotAdded(PropertyNames.User));
        }
        #endregion
        //#region ActivateAccount
        //public async Task<IResult> ActivateAccount(string userId, string code)
        //{
        //    //Check redis cache if userId_rndnumber exists and true
        //    var cacheValue = await _redisCacheManager.GetAsync<string>($"activation_{userId}");
        //    if (cacheValue == null)
        //    {
        //        return new ErrorResult(MessageExtensions.NotFound(PropertyNames.Code));
        //    }
        //    if (cacheValue != code)
        //    {
        //        return new ErrorResult(MessageExtensions.NotCorrect(PropertyNames.Code));
        //    }

        //    //IF true
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return new ErrorResult(MessageExtensions.NotFound(PropertyNames.Email));
        //    }
        //    user.Status = (int)UserStatus.Validated;
        //    var updateResult = await _userManager.UpdateAsync(user);
        //    if (!updateResult.Succeeded)
        //    {
        //        return new ErrorResult(MessageExtensions.AccountNotActivated());
        //    }
        //    return new SuccessResult(MessageExtensions.AccountActivated());
        //}
        //#endregion
    }
}
