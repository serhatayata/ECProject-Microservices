using Core.Entities;

namespace EC.Services.LangResourceAPI.Extensions
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
                options.AddPolicy(name:"langresource_cors", builder =>
                {
                    builder.WithOrigins(
                     sourceOrigin.Baskets,
                     sourceOrigin.Categories,
                     sourceOrigin.Discounts,
                     sourceOrigin.Orders,
                     sourceOrigin.Payments,
                     sourceOrigin.PhotoStocks,
                     sourceOrigin.Products
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }
    }
}
