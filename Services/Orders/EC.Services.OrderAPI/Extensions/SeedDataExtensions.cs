using EC.Services.Order.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.OrderAPI.Extensions
{
    public static class SeedDataExtensions
    {
        private static readonly DateTime datetime_now = DateTime.Now;

        public static void AddSeedData(this IServiceCollection services, ConfigurationManager configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<OrderDbContext>();

            context.Database.Migrate();

            AddOrders(context);

        }

        #region AddCategories
        public static void AddOrders(OrderDbContext context)
        {
            if (!context.Orders.Any())
            {
                Order.Domain.OrderAggregate.Order[] orders =
                {
                    
                };

                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }

        #endregion


    }
}
