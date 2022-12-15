using Core.Entities;
using EC.Services.PhotoStockAPI.Settings;

namespace EC.Services.PhotoStockAPI.Extensions
{
    public static class OptionExtensions
    {
        public static void AddOptionSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ResizeSetting>(configuration.GetSection("ResizeSetting"));
            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));


        }
    }

}
