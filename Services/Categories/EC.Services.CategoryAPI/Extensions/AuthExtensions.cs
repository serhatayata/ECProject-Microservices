using EC.Services.CategoryAPI.Data.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System;

namespace EC.Services.CategoryAPI.Extensions
{
    public static class AuthExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOptions").Get<Core.Utilities.Security.Jwt.TokenOptions>();

            services.AddDbContext<CategoryDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),ServiceLifetime.Scoped);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = tokenOptions.Issuer;
                options.Audience = tokenOptions.Audience;
            });

            services.AddAuthorization(_ =>
            {
                _.AddPolicy("ReadCategory", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "category_read");
                });
                _.AddPolicy("WriteCategory", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "category_write");
                });
                _.AddPolicy("FullCategory", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "category_full");
                });
            });
        }
    }
}
