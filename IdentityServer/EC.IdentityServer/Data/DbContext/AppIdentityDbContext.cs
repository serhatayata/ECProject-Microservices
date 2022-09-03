using EC.IdentityServer.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Utilities.IoC;

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
            });

            modelBuilder.Entity<AppUser>().HasKey(x => x.Id);
            modelBuilder.Entity<AppUser>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<AppUser>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<AppUser>().Property(x => x.UserName).HasMaxLength(20);
            modelBuilder.Entity<AppUser>().Property(x => x.Name).HasMaxLength(100);
            modelBuilder.Entity<AppUser>().Property(x => x.Surname).HasMaxLength(100);
            modelBuilder.Entity<AppUser>().Property(x => x.Surname).HasMaxLength(30);
            modelBuilder.Entity<AppUser>().Property(x => x.Email).HasMaxLength(100);
            modelBuilder.Entity<AppUser>().Property(x => x.PhoneNumber).HasMaxLength(20);
            modelBuilder.Entity<AppUser>().HasIndex(x => x.PhoneNumber).IsUnique();
            #endregion
            #region AppRole
            modelBuilder.Entity<AppRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            modelBuilder.Entity<AppRole>().Property(x => x.Name).HasMaxLength(50);
            #endregion

        }

    }
}
