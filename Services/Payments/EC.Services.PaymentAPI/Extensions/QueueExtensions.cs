using Core.Entities;
using MassTransit;

namespace EC.Services.PaymentAPI.Extensions
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
        //public static void AddRabbitMqConsumer(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var settings = configuration.GetValue<RabbitMqSettings>("RabbitMqSettings");

        //    services.AddMassTransit(x =>
        //    {
        //        x.AddConsumer<ProductChangedEventConsumer>();

        //        // Default Port : 5672
        //        x.UsingRabbitMq((context, cfg) =>
        //        {
        //            cfg.Host(settings.Host, settings.Port, "/", host =>
        //            {
        //                host.Username(settings.Username);
        //                host.Password(settings.Password);
        //            });

        //            cfg.ReceiveEndpoint("product-changed-event-basket-service", e =>
        //            {
        //                e.UseMessageRetry(x=>x.Interval(5,1000));
        //                e.ConfigureConsumer<ProductChangedEventConsumer>(context);
        //                e.ConfigureConsumer<MessageFaultConsumer>(context);
        //            });
        //        });
        //    });
        //}
        #endregion
    }

   
}
