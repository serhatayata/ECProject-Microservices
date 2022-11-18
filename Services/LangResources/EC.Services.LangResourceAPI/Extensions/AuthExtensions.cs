using EC.Services.LangResourceAPI.Data.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.LangResourceAPI.Extensions
{
    public static class AuthExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOptions").Get<Core.Utilities.Security.Jwt.TokenOptions>();

            services.AddDbContext<LangResourceDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.Authority = tokenOptions.Issuer;
            //    options.Audience = tokenOptions.Audience;
            //});

            //services.AddAuthorization(_ =>
            //{
            //    _.AddPolicy("ReadLangResource", policy =>
            //    {
            //        policy.RequireAuthenticatedUser();
            //        policy.RequireClaim("scope", "langresource_read");
            //    });
            //    _.AddPolicy("WriteLangResource", policy =>
            //    {
            //        policy.RequireAuthenticatedUser();
            //        policy.RequireClaim("scope", "langresource_write");
            //    });
            //    _.AddPolicy("FullLangResource", policy =>
            //    {
            //        policy.RequireAuthenticatedUser();
            //        policy.RequireClaim("scope", "langresource_full");
            //    });
            //});
        }

    }
}
