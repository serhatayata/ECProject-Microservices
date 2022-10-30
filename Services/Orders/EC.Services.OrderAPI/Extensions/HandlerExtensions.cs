using EC.Services.Order.Application.Handlers;
using MediatR;

namespace EC.Services.OrderAPI.Extensions
{
    public static class HandlerExtensions
    {
        public static void AddHandlerExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(CreateOrderCommandHandler).Assembly);
            services.AddMediatR(typeof(GetOrdersByUserIdQueryHandler).Assembly);
            services.AddMediatR(typeof(GetOrderByOrderNoQueryHandler).Assembly);


        }

    }
}
