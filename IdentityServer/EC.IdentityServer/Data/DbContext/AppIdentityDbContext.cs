using Core.Utilities.IoC;
using EC.IdentityServer.Models;
using EC.IdentityServer.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EC.IdentityServer.Data.DbContext
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        private readonly IConfiguration? _configuration;

        public AppIdentityDbContext()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("IdentityConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region AppUser
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "Users");

                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasIndex(x => x.UserName).IsUnique();
                entity.Property(x => x.UserName).HasMaxLength(13);
                entity.Property(x => x.Name).HasMaxLength(100);
                entity.Property(x => x.Surname).HasMaxLength(100);
                entity.Property(x => x.Surname).HasMaxLength(30);
                entity.Property(x => x.Email).HasMaxLength(100);
                entity.HasIndex(x=>x.Email).IsUnique();
                entity.Property(x => x.PhoneNumber).HasMaxLength(13);
                entity.HasIndex(x => x.PhoneNumber).IsUnique();

                entity.Property(x => x.CreatedAt).HasDefaultValueSql("(getdate())");
                entity.Property(x => x.UpdatedAt).HasDefaultValueSql("(getdate())");
                entity.Property(x => x.LastSeen).HasDefaultValueSql("(getdate())");
            });
            #endregion
            #region AppRole
            modelBuilder.Entity<AppRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            modelBuilder.Entity<AppRole>().Property(x => x.Name).HasMaxLength(50);
            #endregion
            #region Cards
            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasColumnType("nvarchar(30)");
                entity.Property(x => x.CardNumber).HasColumnType("nvarchar(26)");
                entity.Property(x => x.Cvv).HasColumnType("nvarchar(3)");
            });
            #endregion

        }

    }
}
