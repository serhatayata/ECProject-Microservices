using Core.Utilities.Results;
using EC.IdentityServer.Dtos;
using IResult = Core.Utilities.Results.IResult;

namespace EC.IdentityServer.Services.Abstract
{
    public interface IAuthService
    {
        Task<DataResult<UserDto>> RegisterAsync(RegisterDto model);
        Task<IResult> RegisterActivationAsync(RegisterActivationDto model);

    }
}
