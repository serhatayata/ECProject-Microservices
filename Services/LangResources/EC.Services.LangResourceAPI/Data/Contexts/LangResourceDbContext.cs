using Core.Utilities.IoC;
using EC.Services.LangResourceAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.LangResourceAPI.Data.Contexts
{
    public class LangResourceDbContext:DbContext
    {
        private readonly IConfiguration? _configuration;

        public LangResourceDbContext()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }
        public LangResourceDbContext(DbContextOptions<LangResourceDbContext> options) : base(options)
        {

        }

        public DbSet<LangResource> LangResources { get; set; }
        public DbSet<Lang> Langs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region LangResource
            modelBuilder.Entity<LangResource>(entity =>
            {
                entity.HasKey(x => x.Id);

                //Non-clustered index
                entity.HasIndex(x => x.MessageCode).IsClustered(false);

                entity.Property(x => x.MessageCode).HasColumnType("nvarchar(5)");

                entity.Property(x => x.Tag).HasColumnType("nvarchar(50)");

                entity.Property(x => x.Description).HasColumnType("nvarchar(300)");


            });
            #endregion
            #region Lang
            modelBuilder.Entity<Lang>(entity =>
            {
                entity.HasKey(x => x.Id);

                //Non-clustered index
                entity.HasIndex(x => x.Code).IsUnique().IsClustered(false);

                entity.Property(x => x.Code).HasColumnType("nvarchar(5)");

                entity.Property(x => x.Name).HasColumnType("nvarchar(30)");

            });
            #endregion
        }

    }
}
