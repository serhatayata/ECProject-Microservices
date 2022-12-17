namespace EC.Services.DiscountAPI.Extensions
{
    public static class SeedDataExtensions
    {
        public static void AddSeedData(this IServiceCollection services, ConfigurationManager configuration)
        {
            //var serviceProvider = services.BuildServiceProvider();
            //using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            //var context = scope.ServiceProvider.GetService<CategoryDbContext>();

            //context.Database.Migrate();

            //AddCategories(context);

        }
    }
}
