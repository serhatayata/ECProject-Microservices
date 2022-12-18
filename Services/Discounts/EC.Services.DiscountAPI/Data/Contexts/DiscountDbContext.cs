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
        public DbSet<Campaign> Campaigns { get; set; }

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
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Name).HasColumnType("nvarchar(120)");
                entity.Property(d => d.Description).HasColumnType("nvarchar(500)");

                entity.Property(d => d.Code).HasColumnType("nvarchar(50)");
                entity.HasIndex(d => d.Code).IsUnique().IsClustered(false);

                entity.Property(d => d.DiscountType).HasConversion<int>();
                entity.Property(d => d.Status).HasConversion<byte>();
                entity.Property(d => d.Status).HasDefaultValue((byte)DiscountStatus.Active);

                entity.Property(d => d.CDate).HasDefaultValueSql("getdate()");
                entity.Property(d => d.UDate).HasDefaultValueSql("getdate()");

                entity.Property(d => d.Sponsor).HasDefaultValueSql("nvarchar(100)");
            });
            #endregion
            #region Campaign
            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name).HasColumnType("nvarchar(120)");
                entity.Property(c => c.Description).HasColumnType("nvarchar(500)");

                entity.Property(c => c.Sponsor).HasColumnType("nvarchar(100)");

                entity.Property(c => c.CampaignType).HasConversion<int>();
                entity.Property(c => c.Status).HasConversion<byte>();
                entity.Property(d => d.Status).HasDefaultValue((byte)DiscountStatus.Active);

                entity.Property(c => c).HasColumnType("nvarchar(200)");

                entity.Property(c => c.CDate).HasDefaultValueSql("getdate()");
                entity.Property(c => c.UDate).HasDefaultValueSql("getdate()");
            });
            #endregion
            #region CampaignUser
            modelBuilder.Entity<CampaignUser>(entity =>
            {
                entity.HasKey(cu => new { cu.CampaignId, cu.UserId });

                entity.HasOne(bc => bc.Campaign)
                    .WithMany(b => b.CampaignUsers)
                    .HasForeignKey(bc => bc.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion
        }
    }
}
