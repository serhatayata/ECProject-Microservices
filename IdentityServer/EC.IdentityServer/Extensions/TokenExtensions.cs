using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EC.IdentityServer.Extensions
{
    public class TokenExtensions
    {
        public static string GetTokenTypeValue(string token, string type)
        {
            var handler = new JwtSecurityTokenHandler();
            var authHeader = token.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;

            var value = tokenS?.Claims.FirstOrDefault(x => x.Type == type)?.Value;
            return value;
        }
    }
}
