using EC.Services.ProductAPI.Settings.Abstract;
using EC.Services.ProductAPI.Settings.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace EC.Services.ProductAPI.Extensions
{
    public static class SettingExtensions
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ProductDatabaseSettings>(configuration.GetSection(nameof(ProductDatabaseSettings)));
            services.AddSingleton<IProductDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);
        }
    }
}
