using EC.Services.Order.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.OrderAPI.Extensions
{
    public static class AuthExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOptions").Get<Core.Utilities.Security.Jwt.TokenOptions>();

            services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),ServiceLifetime.Scoped);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = tokenOptions.Issuer;
                options.Audience = tokenOptions.Audience;
            });

            services.AddAuthorization(_ =>
            {
                _.AddPolicy("ReadOrder", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "order_read");
                });
                _.AddPolicy("WriteOrder", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "order_write");
                });
                _.AddPolicy("FullOrder", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "order_full");
                });
            });
        }
    }
}
