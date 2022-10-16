using Core.Utilities.IoC;
using EC.Services.PaymentAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.PaymentAPI.Data.Contexts
{
    public class PaymentDbContext : DbContext
    {
        private readonly IConfiguration? _configuration;

        public PaymentDbContext()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {

        }

        public DbSet<Payment> Payments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Payment
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(x => x.Id);

                //Non-clustered index
                entity.HasIndex(x => x.PaymentNo).IsUnique().IsClustered(false);

                entity.Property(x => x.PaymentNo).HasColumnType("nvarchar(14)");

                entity.Property(x => x.CDate).HasDefaultValueSql("getdate()");

                entity.Property(x => x.Status).HasDefaultValue((int)PaymentStatus.Waiting);

                entity.Property(x => x.PhoneCountry).HasColumnType("nvarchar(2)");

                entity.Property(x => x.PhoneNumber).HasColumnType("nvarchar(10)");

                entity.Property(x => x.UserId).HasColumnType("nvarchar(32)");

                entity.Property(x => x.CardName).HasColumnType("nvarchar(50)");

                entity.Property(x => x.CardNumber).HasColumnType("nvarchar(4)");

                entity.Property(x => x.TotalPrice).HasColumnType("decimal(8,2)");

                entity.Property(x => x.CountryName).HasColumnType("nvarchar(100)");

                entity.Property(x => x.CountyName).HasColumnType("nvarchar(100)");

                entity.Property(x => x.CityName).HasColumnType("nvarchar(100)");

                entity.Property(x => x.AddressDetail).HasColumnType("nvarchar(240)");

                entity.Property(x => x.ZipCode).HasColumnType("nvarchar(5)");

            });
            #endregion
        }


    }
}
