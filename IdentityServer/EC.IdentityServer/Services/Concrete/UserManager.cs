using AutoMapper;
using Core.Extensions;
using Core.Utilities.Results;
using EC.IdentityServer.Constants;
using EC.IdentityServer.Dtos;
using EC.IdentityServer.Models.Identity;
using EC.IdentityServer.Models.Settings;
using EC.IdentityServer.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using IResult = Core.Utilities.Results.IResult;
using EC.IdentityServer.Models.Email;
using MimeKit;

namespace EC.IdentityServer.Services.Concrete
{
    public class UserManager : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private IPasswordHasher<AppUser> _passwordHasher;
        private readonly IMapper _mapper;

        public UserManager(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher, IMapper mapper, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }


        #region ChangePasswordAsync
        public async Task<IResult> ChangePasswordAsync(ChangePasswordDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(PropertyNames.User));
            }
            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (!passwordCheck)
            {
                return new ErrorResult(MessageExtensions.PasswordNotCorrect());
            }
            user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
            var resultUpdate = await _userManager.UpdateAsync(user);
            if (!resultUpdate.Succeeded)
            {
                return new ErrorResult(MessageExtensions.NotChanged(PropertyNames.Password));
            }
            return new SuccessResult(MessageExtensions.Changed(PropertyNames.Password));
        }
        #endregion
        #region ActivateAccountSendSms
        public async Task<IResult> ActivateAccountSendSms(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(PropertyNames.Email));
            }
            Random rnd = new();
            int randomNumber = rnd.Next(100000, 99999);

            var message = new SmtpMessage(
                new List<string>() { user.Email },
                "Account Activation",
                randomNumber.ToString()
                );

            //userId_rndnumber will be added to redis cache

            _emailSender.SendSmtpEmail(message);

            return new SuccessResult();
        }
        #endregion
        #region ActivateAccount
        public async Task<IResult> ActivateAccount(string userId, string code)
        {
            //Check redis cache if userId_rndnumber exists and true
            var cacheValue = 
            if(cacheValue == null)
            {
                return new ErrorResult(MessageExtensions.NotCorrect(PropertyNames.Code));
            }

            //IF true
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ErrorResult(MessageExtensions.NotFound(PropertyNames.Email));
            }
            user.Status = (int)UserStatus.Validated;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return new ErrorResult(MessageExtensions.AccountNotActivated());
            }
            return new SuccessResult(MessageExtensions.AccountActivated());
        }
        #endregion


    }
}
