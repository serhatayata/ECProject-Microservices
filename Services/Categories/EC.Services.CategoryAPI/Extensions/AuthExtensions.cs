﻿using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EC.Services.CategoryAPI.Extensions
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
                _.AddPolicy("ReadCategory", policy => policy.RequireClaim("scope", "category_read"));
                _.AddPolicy("WriteCategory", policy => policy.RequireClaim("scope", "category_write"));
                _.AddPolicy("FullCategory", policy => policy.RequireClaim("scope", "category_full"));
            });
        }
    }
}