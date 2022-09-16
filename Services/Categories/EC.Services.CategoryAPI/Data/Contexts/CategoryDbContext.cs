using Core.Utilities.IoC;
using EC.Services.CategoryAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EC.Services.CategoryAPI.Data.Contexts
{
    public class CategoryDbContext:DbContext
    {
        private readonly IConfiguration? _configuration;

        public CategoryDbContext()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasColumnType("nvarchar(50)");
                entity.Property(x => x.Link).HasColumnType("nvarchar(200)");
                entity.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
            });
            #endregion
        }


    }
}
