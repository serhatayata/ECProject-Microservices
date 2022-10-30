using Core.Utilities.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Infrastructure
{
    public class OrderDbContext:DbContext
    {
        private readonly IConfiguration? _configuration;

        public OrderDbContext()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }


        public DbSet<Domain.OrderAggregate.Order> Orders { get; set; }
        public DbSet<Domain.OrderAggregate.OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Order
            modelBuilder.Entity<Domain.OrderAggregate.Order>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.UserId).HasColumnType("nvarchar(40)");
                //Non-clustered index
                entity.HasIndex(x => x.OrderNo).IsUnique().IsClustered(false);
                entity.Property(x => x.OrderNo).HasColumnType("nvarchar(12)");

                entity.Property(x => x.CDate).HasDefaultValueSql("getdate()");

                entity.Property(x => x.CountryName).HasColumnType("nvarchar(100)");
                entity.Property(x => x.CountyName).HasColumnType("nvarchar(100)");
                entity.Property(x => x.CityName).HasColumnType("nvarchar(100)");
                entity.Property(x => x.AddressDetail).HasColumnType("nvarchar(240)");
                entity.Property(x => x.ZipCode).HasColumnType("nvarchar(5)");
            });
            #endregion
            #region OrderItem
            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.ProductId).HasColumnType("nvarchar(12)");
                entity.Property(x => x.Price).HasColumnType("decimal(8,2)");

                entity.HasOne(bc => bc.Order)
                    .WithMany(b => b.OrderItems)
                    .HasForeignKey(bc => bc.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }

}
