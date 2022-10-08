using EC.Services.DiscountAPI.Settings;

namespace EC.Services.DiscountAPI.Extensions
{
    public static class SettingExtensions
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DiscountDatabaseSettings>(configuration.GetSection(nameof(DiscountDatabaseSettings)));

        }
    }
}
