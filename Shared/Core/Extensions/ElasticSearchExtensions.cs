using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Core.CrossCuttingConcerns.Logging;

namespace Core.Extensions
{
    public static class ElasticSearchExtensions
    {

        #region AddELKLogSettings
        public static void AddELKLogSettings(IServiceCollection services)
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
                            .WriteTo.Elasticsearch(ConfigureELS(configuration, env))
                            .CreateLogger();
        }
        #endregion
        #region ConfigureELS
        static ElasticsearchSinkOptions ConfigureELS(IConfigurationRoot configuration, string env)
        {
            var options = new ElasticsearchSinkOptions(new Uri(configuration["ELKConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                //Index format dll ismini alıyoruz Assembly'den
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName()?.Name}-{env.ToLower().Replace(".", "-")}"
            };
            return options;
        }
        #endregion
        #region AddElasticSearch
        public static void AddElasticSearch(IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ELKConfiguration:Uri"];
            var defaultIndex = configuration["ELKConfiguration:index"];

            var settings = new ConnectionSettings(new Uri(url))
                                .PrettyJson()
                                .DefaultIndex(defaultIndex);

            //AddDefaultMappings(settings);

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }
        #endregion
        #region AddDefaultMappings
        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            //settings.DefaultMappingFor<LogDetail>(p =>
            //    p.Ignore(x => x.Price)
            //     //.Ignore(x=>x.Id)  
            //     .Ignore(x => x.Quantity)
            //);
        }
        #endregion
        #region CreateIndex
        private static void CreateIndex(IElasticClient client, string indexName)
        {
            client.Indices.Create(indexName, i => i.Map<LogDetail>(x => x.AutoMap()));
        }
        #endregion

    }
}
