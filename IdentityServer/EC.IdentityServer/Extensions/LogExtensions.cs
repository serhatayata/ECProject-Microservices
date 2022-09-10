using Serilog;

namespace EC.IdentityServer.Extensions
{
    public class LogExtensions
    {
        public static void ConfigureLogs()
        {
            //Get the environment which the app is running on
            var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //Get Config
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .Build();
            //Create logger
            Log.Logger = new LoggerConfiguration()
                            //.Enrich.FromLogContext()
                            //.Enrich.WithExceptionDetails() // Detail exception
                            //.WriteTo.Elasticsearch(ConfigureELS(configuration, env))
                            .CreateLogger();
        }
    }
}
