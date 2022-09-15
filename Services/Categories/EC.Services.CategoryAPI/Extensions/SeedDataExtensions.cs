using EC.Services.CategoryAPI.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace EC.Services.CategoryAPI.Extensions
{
    public static class SeedDataExtensions
    {
        public static void AddSeedData(CategoryDbContext context)
        {
            context.Database.Migrate();
            var now = DateTime.Now;



        }

    }
}
