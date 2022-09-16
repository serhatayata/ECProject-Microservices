using Core.Extensions;
using EC.Services.CategoryAPI.Data.Contexts;
using EC.Services.CategoryAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EC.Services.CategoryAPI.Extensions
{
    public static class SeedDataExtensions
    {
        public static void AddSeedData(CategoryDbContext context)
        {
            context.Database.Migrate();
            var datetime_now = DateTime.Now;

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

    }
}
