using EC.Services.PhotoStockAPI.Entities.Options;

namespace EC.Services.PhotoStockAPI.Extensions
{
    public static class OptionExtensions
    {
        public static void AddOptionSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ResizeSetting>(configuration.GetSection("ResizeSetting"));


        }
    }

}
