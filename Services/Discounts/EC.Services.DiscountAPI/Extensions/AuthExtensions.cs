using EC.Services.DiscountAPI.Data.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.DiscountAPI.Extensions
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
                _.AddPolicy("ReadDiscount", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "discount_read");
                });
                _.AddPolicy("WriteDiscount", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "discount_write");
                });
                _.AddPolicy("FullDiscount", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "discount_full");
                });
            });
        }

    }
}
