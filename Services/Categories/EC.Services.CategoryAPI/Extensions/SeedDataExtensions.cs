using Core.Extensions;
using EC.Services.CategoryAPI.Data.Contexts;
using EC.Services.CategoryAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Text;

namespace EC.Services.CategoryAPI.Extensions
{
    public static class SeedDataExtensions
    {
        private static readonly DateTime datetime_now = DateTime.Now;

        public static void AddSeedData(this IServiceCollection services ,ConfigurationManager configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<CategoryDbContext>();

            context.Database.Migrate();

            AddCategories(context);

        }

        #region AddCategories
        public static void AddCategories(CategoryDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category()
                    {
                        Name="Category_1",
                        Line=1,
                        Link=SeoLinkExtensions.GenerateSlug("Category_1"),
                        CreatedAt=datetime_now
                    },
                    new Category()
                    {
                        Name="Category_2",
                        Line=2,
                        Link=SeoLinkExtensions.GenerateSlug("Category_2"),
                        CreatedAt=datetime_now
                    },
                    new Category()
                    {
                        Name="SubCategory_1",
                        Line=3,
                        Link=SeoLinkExtensions.GenerateSlug("SubCategory_1"),
                        CreatedAt=datetime_now
                    },
                    new Category()
                    {
                        Name="SubCategory_2",
                        Line=3,
                        Link=SeoLinkExtensions.GenerateSlug("SubCategory_2"),
                        CreatedAt=datetime_now
                    }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }

        #endregion

    }
}
