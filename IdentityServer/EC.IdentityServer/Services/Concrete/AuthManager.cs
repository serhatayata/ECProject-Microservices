using AutoMapper;
using Core.Aspects.Autofac.Transaction;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Entities;
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
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly SourceOrigin _sourceOrigin;
        private readonly IMapper _mapper;

        public AuthManager(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, ICommunicationApiService commApiService, IRedisCacheManager redisCacheManager, SourceOrigin sourceOrigin, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _commApiService = commApiService;
            _redisCacheManager = redisCacheManager;
            _sourceOrigin = sourceOrigin;
            _mapper = mapper;
        }

        #region RegisterAsync
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> RegisterAsync(RegisterDto model)
        {
            var validatedPhoneNumber = PhoneNumberExtensions.ValidatePhoneNumber(model.PhoneNumber, model.CountryCode);
            if (!validatedPhoneNumber.Success)
                return new ErrorResult(MessageExtensions.NotCorrect(PropertyNames.PhoneNumber));

            var formattedPhoneNumber = validatedPhoneNumber.Data.FormattedNumber.Substring(1);

            var userPhoneNumberExists = await _userManager.FindByNameAsync(formattedPhoneNumber);
            if (userPhoneNumberExists != null)
                return new ErrorResult(MessageExtensions.AlreadyExists(PropertyNames.PhoneNumber));

            var userEmailExists = await _userManager.FindByEmailAsync(model.PhoneNumber);
            if (userEmailExists != null)
                return new ErrorResult(MessageExtensions.AlreadyExists(PropertyNames.Email));

            var userAdded = _mapper.Map<AppUser>(model);
            userAdded.UserName = formattedPhoneNumber;
            userAdded.Status = (byte)UserStatus.NotValidated;
            var result = await _userManager.CreateAsync(userAdded, model.Password);
            if (result.Succeeded)
            {
                string userRole = "User.Normal";
                var roleResult = await _userManager.AddToRoleAsync(userAdded, userRole);

                string activationCode = RandomExtensions.RandomCode(6);
                await _redisCacheManager.SetAsync($"auth-register-activation-code-{userAdded.Id}", activationCode);
                EmailData emailData = new()
                {
                    EmailToId = userAdded.Email,
                    EmailToName = $"ECProject - {userAdded.UserName} - {userAdded.Name} {userAdded.Surname}",
                    EmailSubject = $"User Activation - {userAdded.UserName}",
                    EmailBody = $"User Activation Code : {activationCode}"
                };
                await _commApiService.SendSmtpEmailAsync(emailData);
                return new SuccessResult(MessageExtensions.Sent(PropertyNames.User));
            }
            return new ErrorResult(MessageExtensions.NotCompleted(PropertyNames.Register));
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
