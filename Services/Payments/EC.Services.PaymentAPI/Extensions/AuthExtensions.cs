using EC.Services.PaymentAPI.Data.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.PaymentAPI.Extensions
{
    public static class AuthExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOptions").Get<Core.Utilities.Security.Jwt.TokenOptions>();

            services.AddDbContext<PaymentDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = tokenOptions.Issuer;
                options.Audience = tokenOptions.Audience;
            });

            services.AddAuthorization(_ =>
            {
                _.AddPolicy("ReadPayment", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "payment_read");
                });
                _.AddPolicy("WritePayment", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "payment_write");
                });
                _.AddPolicy("FullPayment", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "payment_full");
                });
            });
        }
    }
}
