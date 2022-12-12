using AutoMapper;
using Core.Aspects.Autofac.Transaction;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Entities;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using EC.IdentityServer.Constants;
using EC.IdentityServer.Dtos;
using EC.IdentityServer.Models.Identity;
using EC.IdentityServer.Publishers;
using EC.IdentityServer.Services.Abstract;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using IResult = Core.Utilities.Results.IResult;

namespace EC.IdentityServer.Services.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly SourceOrigin _sourceOrigin;
        private readonly IMapper _mapper;
        private readonly RedisConfigurationSettings _redisConfiguration;

        public AuthManager(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, RabbitMQPublisher rabbitMQPublisher, IRedisCacheManager redisCacheManager, SourceOrigin sourceOrigin, IMapper mapper, IOptions<RedisConfigurationSettings> redisConfiguration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _rabbitMQPublisher = rabbitMQPublisher;
            _redisCacheManager = redisCacheManager;
            _sourceOrigin = sourceOrigin;
            _mapper = mapper;
            _redisConfiguration = redisConfiguration.Value;
        }


        #region RegisterAsync
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        public async Task<DataResult<UserDto>> RegisterAsync(RegisterDto model)
        {
            var validatedPhoneNumber = PhoneNumberExtensions.ValidatePhoneNumber(model.PhoneNumber, model.CountryCode);
            if (!validatedPhoneNumber.Success)
                return new ErrorDataResult<UserDto>(MessageExtensions.NotCorrect(PropertyNames.PhoneNumber));

            var formattedPhoneNumber = validatedPhoneNumber.Data.FormattedNumber.Substring(1);

            var userPhoneNumberExists = await _userManager.FindByNameAsync(formattedPhoneNumber);
            if (userPhoneNumberExists != null)
                return new ErrorDataResult<UserDto>(MessageExtensions.AlreadyExists(PropertyNames.PhoneNumber));

            var userEmailExists = await _userManager.FindByEmailAsync(model.PhoneNumber);
            if (userEmailExists != null)
                return new ErrorDataResult<UserDto>(MessageExtensions.AlreadyExists(PropertyNames.Email));

            var conn = _redisCacheManager.GetDatabase(_redisConfiguration.IdentityRedisDb);
            if (conn == null)
                return new ErrorDataResult<UserDto>(MessageExtensions.NotCompleted(PropertyNames.Register));

            var userAdded = _mapper.Map<AppUser>(model);
            userAdded.EmailConfirmed = true; //Email validation will be just used for activation
            userAdded.UserName = formattedPhoneNumber;
            userAdded.Status = (byte)UserStatus.NotValidated;
            var result = await _userManager.CreateAsync(userAdded, model.Password);
            if (result.Succeeded)
            {
                string userRole = "User.Normal";
                var roleResult = await _userManager.AddToRoleAsync(userAdded, userRole);

                string redisActivationKey = $"auth-register-activation-code-{userAdded.Id}";

                string activationCode = RandomExtensions.RandomCode(10);
                var setValue = await _redisCacheManager.GetDatabase(_redisConfiguration.IdentityRedisDb).StringSetAsync(redisActivationKey, activationCode, TimeSpan.FromMinutes(10));

                if (!setValue)
                    throw new Exception(MessageExtensions.NotCompleted(PropertyNames.Register));

                EmailData emailData = new()
                {
                    EmailToId = userAdded.Email,
                    EmailToName = $"ECProject - {userAdded.UserName} - {userAdded.Name} {userAdded.Surname}",
                    EmailSubject = $"User Activation - {userAdded.UserName}",
                    EmailTextBody = $"User Activation Code : {activationCode}"
                };
                _rabbitMQPublisher.EmailSmtpSendPublish(emailData);

                UserDto user = new() { Id = userAdded.Id };
                return new SuccessDataResult<UserDto>(user, MessageExtensions.Sent(PropertyNames.User));
            }
            return new ErrorDataResult<UserDto>(MessageExtensions.NotCompleted(PropertyNames.Register));
        }
        #endregion
        #region RegisterActivationAsync
        public async Task<IResult> RegisterActivationAsync(RegisterActivationDto model)
        {
            string redisActivationKey = $"auth-register-activation-code-{model.Id}";

            var redisConn = _redisCacheManager.GetDatabase(_redisConfiguration.IdentityRedisDb);
            if (redisConn == null)
                return new ErrorResult(MessageExtensions.NotCompleted(PropertyNames.RegisterActivation));

            var activationCode = await redisConn.StringGetAsync(redisActivationKey);
            if(string.IsNullOrEmpty(activationCode))
                return new ErrorResult(MessageExtensions.NotFound(PropertyNames.RegisterActivationCode));

            if(activationCode != model.ActivationCode)
                return new ErrorResult(MessageExtensions.NotCorrect(PropertyNames.RegisterActivationCode));

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
            if (user == null)
                return new ErrorResult(MessageExtensions.NotFound(PropertyNames.User));

            user.Status = (int)UserStatus.Validated;
            var userUpdated = await _userManager.UpdateAsync(user);
            if (!userUpdated.Succeeded)
                return new ErrorResult(MessageExtensions.NotCompleted(PropertyNames.RegisterActivation));

            await redisConn.KeyDeleteAsync(redisActivationKey);

            return new SuccessResult(MessageExtensions.Completed(PropertyNames.RegisterActivation));
        }
        #endregion


    }
}
