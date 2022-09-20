using Autofac;
using Core.DataAccess.Dapper;
using EC.Services.PhotoStockAPI.DependencyResolvers.Autofac;

namespace EC.Services.PhotoStockAPI.Extensions
{
    public static class AutofacExtensions
    {
        public static void AddAutofacSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("DefaultConnection");

            var autofacBuilder = new ContainerBuilder();
            autofacBuilder.RegisterModule(new AutofacBusinessModule());
        }
    }
}
