using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Core.Utilities.Security.Jwt//Jwt : Json Web Token
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(IdentityUser user, List<Claim> rolesForClaims, int companyId=0);
        RefreshToken CreateRefreshToken(IdentityUser user,List<Claim> claims=null, int companyId=0);
    }
}