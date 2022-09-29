using Core.Entities;
using EC.Services.ProductAPI.Settings.Concrete;
using MassTransit;

namespace EC.Services.ProductAPI.Extensions
{
    public static class QueueExtensions
    {
        #region AddRabbitMqProducer
        public static void AddRabbitMqProducer(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetValue<RabbitMqSettings>("RabbitMqSettings");

            services.AddMassTransit(x =>
            {
                // Default Port : 5672
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(settings.Host, settings.Port, "/", host =>
                    {
                        host.Username(settings.Username);
                        host.Password(settings.Password);
                    });
                });
            });
        }
        #endregion

    }
}
