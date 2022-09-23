using Core.Extensions;
using EC.Services.PhotoStockAPI.Data.Contexts;
using EC.Services.PhotoStockAPI.Dtos;
using EC.Services.PhotoStockAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.PhotoStockAPI.Extensions
{
    public static class SeedDataExtensions
    {
        private static readonly DateTime datetime_now = DateTime.Now;

        public static void AddSeedData(this IServiceCollection services, ConfigurationManager configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<PhotoStockDbContext>();

            context.Database.Migrate();

            AddCategories(context);

        }

        #region AddPhotos
        public static void AddCategories(PhotoStockDbContext context)
        {
            if (!context.Photos.Any())
            {
                var photos = new[]
                {
                    new Photo()
                    {
                        EntityId=1,
                        PhotoType=(int)PhotoTypes.Category,
                        Url="deneme_url_1"
                    },
                    new Photo()
                    {
                        EntityId=2,
                        PhotoType=(int)PhotoTypes.Product,
                        Url="deneme_url_2"
                    },
                    new Photo()
                    {
                        EntityId=3,
                        PhotoType=(int)PhotoTypes.Category,
                        Url="deneme_url_3"
                    },
                };

                context.Photos.AddRange(photos);
                context.SaveChanges();
            }
        }

        #endregion

    }

}
