using Core.Entities;
using EC.Services.Order.Application.Consumers;
using EC.Services.Order.Application.Handlers;
using MassTransit;
using MediatR;

namespace EC.Services.OrderAPI.Extensions
{
    public static class QueueExtensions
    {
        #region AddMassTransitSettings
        public static void AddMassTransitSettings(this IServiceCollection services, IConfiguration configuration)
        {
            //var settings = configuration.GetValue<RabbitMqSettings>("RabbitMqSettings");

            var settings = configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();

            services.AddMassTransit(x =>
            {
                string userName = settings.Username;
                string password = settings.Password;
                string host = settings.Host;
                ushort port = settings.Port;

                x.AddConsumer<CreateOrderCommandConsumer>();

                // Default Port : 5672
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(settings.Host, settings.Port, "/", host =>
                    {
                        host.Username(settings.Username);
                        host.Password(settings.Password);
                    });

                    cfg.ReceiveEndpoint("create-order-service", e =>
                    {
                        e.ConfigureConsumer<CreateOrderCommandConsumer>(context);
                    });
                });

                //x.UsingRabbitMq((context, cfg) =>
                //{
                //    cfg.Host(configuration["RabbitMqSettings:RabbitMQUrl"], "/", host =>
                //    {
                //        host.Username(userName);
                //        host.Password(password);
                //    });

                //    cfg.ReceiveEndpoint("create-order-service", e =>
                //    {
                //        e.ConfigureConsumer<CreateOrderCommandConsumer>(context);
                //    });
                //});
            });
        }
        #endregion
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
