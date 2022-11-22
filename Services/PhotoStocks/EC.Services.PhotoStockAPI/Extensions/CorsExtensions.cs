using Core.Entities;

namespace EC.Services.PhotoStockAPI.Extensions
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
                options.AddPolicy(name: "photostock_cors", builder =>
                {
                    builder.WithOrigins(
                     sourceOrigin.Orders,
                     sourceOrigin.Baskets,
                     sourceOrigin.Products,
                     sourceOrigin.Categories,
                     sourceOrigin.Gateway
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }
    }
}
