using Core.Extensions;
using EC.Services.PaymentAPI.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.PaymentAPI.Extensions
{
    public static class SeedDataExtensions
    {
        private static readonly DateTime datetime_now = DateTime.Now;

        public static void AddSeedData(this IServiceCollection services, ConfigurationManager configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<PaymentDbContext>();

            context.Database.Migrate();

            AddCategories(context);

        }

        #region AddCategories
        public static void AddCategories(PaymentDbContext context)
        {
            //if (!context.Payments.Any())
            //{
                

            //    context.Payments.AddRange(categories);
            //    context.SaveChanges();
            //}
        }

        #endregion

    }

}
