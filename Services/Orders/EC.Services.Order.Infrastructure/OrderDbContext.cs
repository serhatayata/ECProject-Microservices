using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Infrastructure
{
    public class OrderDbContext:DbContext
    {
        public const string DEFAULT_SCHEMA = "ordering";

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }

        public DbSet<Domain.OrderAggregate.Order> Orders { get; set; }
        public DbSet<Domain.OrderAggregate.OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Order
            modelBuilder.Entity<Domain.OrderAggregate.Order>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.BuyerId).HasColumnType("nvarchar(12)");
                //Non-clustered index
                entity.HasIndex(x => x.OrderNo).IsUnique().IsClustered(false);
                entity.Property(x => x.OrderNo).HasColumnType("nvarchar(12)");

                entity.Property(x => x.CDate).HasDefaultValueSql("getdate()");

                entity.Property(x => x.CountryName).HasColumnType("nvarchar(100)");
                entity.Property(x => x.CountyName).HasColumnType("nvarchar(100)");
                entity.Property(x => x.CityName).HasColumnType("nvarchar(100)");
                entity.Property(x => x.AddressDetail).HasColumnType("nvarchar(240)");
                entity.Property(x => x.ZipCode).HasColumnType("nvarchar(5)");

                entity.ToTable("Orders", DEFAULT_SCHEMA);


            });
            #endregion
            #region OrderItem
            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>(entity =>
            {
                entity.HasKey(x=>x.Id);

                entity.Property(x => x.ProductId).HasColumnType("nvarchar(12)");
                entity.Property(x => x.Price).HasColumnType("decimal(8,2)");
            });
            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}
