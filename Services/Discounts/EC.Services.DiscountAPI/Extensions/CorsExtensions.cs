using Core.Entities;

namespace EC.Services.DiscountAPI.Extensions
{
    public static class CorsExtensions
    {
        public static void AddCorsSettings(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            SourceOrigin sourceOrigin = new();
            if (env.IsDevelopment())
            {
                sourceOrigin = configuration.GetSection("SourceOriginSettings").Get<SourceOriginSettings>().Development;
            }
            else
            {
                sourceOrigin = configuration.GetSection("SourceOriginSettings").Get<SourceOriginSettings>().Product;
            }

            services.AddCors(options =>
            {
                options.AddPolicy(name: "discount_cors", builder =>
                {
                    builder.WithOrigins(
                     sourceOrigin.Discounts,
                     sourceOrigin.Orders,
                     sourceOrigin.Payments,
                     sourceOrigin.Baskets,
                     sourceOrigin.Products,
                     sourceOrigin.Gateway
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }
    }
}
