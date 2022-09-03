using EC.IdentityServer.Configuration;
using EC.IdentityServer.Data.DbContext;
using EC.IdentityServer.Models.Identity;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace EC.IdentityServer.Data.SeedData
{
    public static class ConfigurationDbContextSeed
    {
        public async static Task AddIdentityConfigurationSeedData(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
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

    }
}
