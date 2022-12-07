using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EC.Services.Communications.Extensions
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
                _.AddPolicy("ReadCommunication", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "communication_read");
                });
                _.AddPolicy("WriteCommunication", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "communication_write");
                });
                _.AddPolicy("FullCommunication", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "communication_full");
                });
            });
        }
    }
}
