using EC.Services.PhotoStockAPI.Data.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.PhotoStockAPI.Extensions
{
    public static class AuthExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOptions").Get<Core.Utilities.Security.Jwt.TokenOptions>();

            services.AddDbContext<PhotoStockDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = tokenOptions.Issuer;
                options.Audience = tokenOptions.Audience;
            });

            services.AddAuthorization(_ =>
            {
                _.AddPolicy("ReadPhotoStock", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "photostock_read");
                });
                _.AddPolicy("WritePhotoStock", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "photostock_write");
                });
                _.AddPolicy("FullPhotoStock", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "photostock_full");
                });
            });
        }
    }
}
