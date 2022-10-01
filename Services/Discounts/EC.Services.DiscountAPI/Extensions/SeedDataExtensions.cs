using Core.Extensions;
using EC.Services.DiscountAPI.Data.Contexts;
using EC.Services.DiscountAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.DiscountAPI.Extensions
{
    public static class SeedDataExtensions
    {
        private static readonly DateTime datetime_now = DateTime.Now;

        public static void AddSeedData(this IServiceCollection services, ConfigurationManager configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<DiscountDbContext>();

            context.Database.Migrate();
        }

        #region AddDiscounts
        public static void AddDiscounts(DiscountDbContext context)
        {
            if (!context.Discounts.Any())
            {
                var discounts = new[]
                {
                    new Discount()
                    {
                        UserId="1"
                    }
                };

                context.Discounts.AddRange(discounts);
                context.SaveChanges();
            }
        }

        #endregion

    }

}
