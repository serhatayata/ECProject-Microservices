using EC.IdentityServer.Data.DbContext;
using EC.IdentityServer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EC.IdentityServer.Extensions
{
    public static class IdentityExtensions
    {
        public static void AddDbContextSettings(string connectionString, IConfiguration Configuration)
        {
            var assembly = typeof(Program).Assembly.GetName().Name;

            var services = new ServiceCollection();

            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"), b => b.MigrationsAssembly(assembly)));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                //this place might be changed...
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
              .AddAspNetIdentity<AppUser>()
              .AddConfigurationStore(options =>
              {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(assembly));
              }).AddOperationalStore(options =>
                {
                  options.ConfigureDbContext = b =>
                              b.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(assembly));
                })
                .AddDeveloperSigningCredential(); //Sertifika yoksa
        }
    }
}
