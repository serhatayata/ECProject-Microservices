using Core.Entities;
using EC.IdentityServer.Models.Settings;

namespace EC.IdentityServer.Extensions
{
    public static class OptionsExtensions
    {
        public static void AddOptionsPattern(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpSetting>(configuration.GetSection("SmtpSetting"));
            services.Configure<RedisConfigurationSettings>(configuration.GetSection("RedisConfiguration"));

        }
    }
}
