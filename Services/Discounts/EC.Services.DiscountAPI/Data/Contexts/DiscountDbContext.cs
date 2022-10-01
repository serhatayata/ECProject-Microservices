using Core.Utilities.IoC;
using EC.Services.DiscountAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.DiscountAPI.Data.Contexts
{
    public class DiscountDbContext:DbContext
    {
        private readonly IConfiguration? _configuration;

        public DiscountDbContext()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }

        public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
        {

        }

        public DbSet<Discount> Discounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Discount
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Code).HasColumnType("nvarchar(50)");
                entity.Property(x => x.CDate).HasColumnType("datetime2").HasDefaultValueSql("getdate()");
            });
            #endregion
        }
    }
}
