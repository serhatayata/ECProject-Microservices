﻿using EC.IdentityServer.Dtos;
using IResult = Core.Utilities.Results.IResult;
namespace EC.IdentityServer.Services.Abstract
{
    public interface IUserService
    {
        Task<IResult> ChangePasswordAsync(ChangePasswordDto model);


    }
}
