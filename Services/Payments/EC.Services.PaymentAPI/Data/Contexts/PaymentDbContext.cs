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
            //modelBuilder.Entity<Payment>(entity =>
            //{
            //    entity.HasKey(x => x.Id);
            //    entity.Property(x => x.Name).HasColumnType("nvarchar(50)");
            //    entity.Property(x => x.Link).HasColumnType("nvarchar(200)");
            //    entity.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
            //});
            #endregion
        }


    }
}
