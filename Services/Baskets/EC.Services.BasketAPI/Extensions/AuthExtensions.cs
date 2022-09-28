using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EC.Services.BasketAPI.Extensions
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
                _.AddPolicy("ReadBasket", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "basket_read");
                });
                _.AddPolicy("WriteBasket", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "basket_write");
                });
                _.AddPolicy("FullBasket", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "basket_full");
                });
            });
        }

    }
}
