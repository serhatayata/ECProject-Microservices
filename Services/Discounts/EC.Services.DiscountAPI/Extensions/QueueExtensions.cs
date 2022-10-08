using Core.Entities;
using EC.Services.DiscountAPI.Consumers;
using MassTransit;

namespace EC.Services.DiscountAPI.Extensions
{
    public static class QueueExtensions
    {
        #region AddRabbitMqProducer
        //public static void AddRabbitMqProducer(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var settings = configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();

        //    services.AddMassTransit(x =>
        //    {
        //        // Default Port : 5672
        //        x.UsingRabbitMq((context, cfg) =>
        //        {
        //            cfg.Host(settings.Host, settings.Port, "/", host =>
        //            {
        //                host.Username(settings.Username);
        //                host.Password(settings.Password);
        //            });
        //        });
        //    });
        //}
        #endregion
        #region AddRabbitMqConsumer
        public static void AddRabbitMqConsumer(this IServiceCollection services, IConfiguration configuration)
        {
            var settings= configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductDeletedEventConsumer>();
                // Default Port : 5672
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(settings.Host, settings.Port, "/", host =>
                    {
                        host.Username(settings.Username);
                        host.Password(settings.Password);
                    });

                    cfg.ReceiveEndpoint("product-deleted-event-service", e =>
                    {
                        e.ConfigureConsumer<ProductDeletedEventConsumer>(context);
                    });
                });
            });
        }
        #endregion

    }
}
