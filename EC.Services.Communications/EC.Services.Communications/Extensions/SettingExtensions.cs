using EC.Services.Communications.Models.Settings;

namespace EC.Services.Communications.Extensions
{
    public static class SettingExtensions
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        }
    }
}
