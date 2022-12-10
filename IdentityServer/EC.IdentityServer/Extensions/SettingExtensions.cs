using Core.Entities;

namespace EC.IdentityServer.Extensions
{
    public static class SettingExtensions
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqSettings>(configuration.GetSection(nameof(RabbitMqSettings)));

        }
    }
}
