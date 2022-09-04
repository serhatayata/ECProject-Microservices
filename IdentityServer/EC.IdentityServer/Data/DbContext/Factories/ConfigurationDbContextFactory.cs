using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EC.IdentityServer.Data.DbContext.Factories
{
    public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var assembly = typeof(Program).Assembly.GetName().Name;

            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            var storeOptions = new ConfigurationStoreOptions();

            var connectionString = config.GetConnectionString("IdentityConnection");

            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly(assembly));

            return new ConfigurationDbContext(optionsBuilder.Options, storeOptions);
        }
    }
}
