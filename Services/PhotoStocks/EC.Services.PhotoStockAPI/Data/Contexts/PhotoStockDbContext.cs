using Core.Utilities.IoC;
using EC.Services.PhotoStockAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.PhotoStockAPI.Data.Contexts
{
    public class PhotoStockDbContext : DbContext
    {
        private readonly IConfiguration? _configuration;

        public PhotoStockDbContext()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }
        public PhotoStockDbContext(DbContextOptions<PhotoStockDbContext> options) : base(options)
        {

        }

        public DbSet<Photo> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Photo
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Url).HasColumnType("nvarchar(500)");
                entity.Property(x => x.PhotoType).HasColumnType("tinyint");
            });
            #endregion
        }

    }
}
