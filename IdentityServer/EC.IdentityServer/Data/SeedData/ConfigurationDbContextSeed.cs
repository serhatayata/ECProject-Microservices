using EC.IdentityServer.Configuration;
using EC.IdentityServer.Data.DbContext;
using EC.IdentityServer.Models.Identity;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace EC.IdentityServer.Data.SeedData
{
    public class ConfigurationDbContextSeed
    {
        public async static Task AddIdentityConfigurationSettingsAsync(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            var assembly = typeof(ConfigurationDbContextSeed).Assembly.GetName().Name;
            var identityConnString = configuration.GetConnectionString("IdentityConnection");

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

            #region Different way of AddIdentityServer

            //services.AddOperationalDbContext(
            //        options =>
            //        {
            //            options.ConfigureDbContext = db =>
            //                db.UseSqlServer(
            //                    identityConnString,
            //                    sql => sql.MigrationsAssembly(typeof(ConfigurationDbContextSeed).Assembly.FullName)
            //                );
            //        }
            //);

            //services.AddConfigurationDbContext(
            //    options =>
            //    {
            //        options.ConfigureDbContext = db =>
            //            db.UseSqlServer(
            //                identityConnString,
            //                sql => sql.MigrationsAssembly(typeof(ConfigurationDbContextSeed).Assembly.FullName));
            //    }
            //);
            #endregion


            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                //see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
              .AddAspNetIdentity<AppUser>()
              .AddConfigurationStore(options =>
              {
                  options.ConfigureDbContext = b => b.UseSqlServer(identityConnString, opt => opt.MigrationsAssembly(assembly));
              }).AddOperationalStore(options =>
              {
                  options.ConfigureDbContext = b =>
                              b.UseSqlServer(identityConnString, opt => opt.MigrationsAssembly(assembly));
              })
                .AddDeveloperSigningCredential(); //Sertifika yoksa

            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var persistedGrantDbContext = scope.ServiceProvider.GetService<PersistedGrantDbContext>();
            persistedGrantDbContext.Database.Migrate();
            var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();

            var clientUrls = new Dictionary<string, string>();

            clientUrls.Add("Mvc", configuration.GetValue<string>("MvcClient"));
            clientUrls.Add("Spa", configuration.GetValue<string>("SpaClient"));

            if (!(await context.Clients.AnyAsync()))
            {
                foreach (var client in Config.GetClients(clientUrls))
                {
                    await context.Clients.AddAsync(client.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!(await context.IdentityResources.AnyAsync()))
            {
                foreach (var resource in Config.IdentityResources.ToList())
                {
                    await context.IdentityResources.AddAsync(resource.ToEntity());
                }

                await context.SaveChangesAsync();
            }

            if (!(await context.ApiScopes.AnyAsync()))
            {
                foreach (var resource in Config.ApiScopes.ToList())
                {
                    await context.ApiScopes.AddAsync(resource.ToEntity());
                }

                await context.SaveChangesAsync();
            }

            if (!(await context.ApiResources.AnyAsync()))
            {
                foreach (var resource in Config.ApiResources.ToList())
                {
                    await context.ApiResources.AddAsync(resource.ToEntity());
                }

                await context.SaveChangesAsync();
            }
        }


    }
}
