using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EC.IdentityServer.Extensions
{
    public class TokenExtensions
    {
        public static string GetTokenTypeValue(string token, string type)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenExt = token.Substring(7);
            JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            IEnumerable<Claim> claims = securityToken.Claims;

            var value = claims.FirstOrDefault(x => x.Type == type)?.Value;
            return value;
        }
    }
}
