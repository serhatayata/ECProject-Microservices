using Microsoft.AspNetCore.Authentication.JwtBearer;
using Nest;

namespace EC.Services.ProductAPI.Extensions
{
    public static class AuthExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOptions").Get<Core.Utilities.Security.Jwt.TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = tokenOptions.Issuer;
                options.Audience = tokenOptions.Audience;
            });

            services.AddAuthorization(_ =>
            {
                _.AddPolicy("ReadProduct", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "product_read");
                });
                _.AddPolicy("WriteProduct", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "product_write");
                });
                _.AddPolicy("FullProduct", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "product_full");
                });
            });
        }
    }
}
