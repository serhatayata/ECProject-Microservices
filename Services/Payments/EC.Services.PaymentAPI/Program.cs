using Autofac.Extensions.DependencyInjection;
using Autofac;
using EC.Services.PaymentAPI.Extensions;
using EC.Services.PaymentAPI.Mappings;
using EC.Services.PaymentAPI.DependencyResolvers.Autofac;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.Entities.ElasticSearch.Abstract;
using Core.Entities.ElasticSearch.Concrete;
using Core.Extensions;
using Serilog;
using Core.DependencyResolvers;
using Core.Utilities.IoC;
using Core.Entities;
using EC.Services.PaymentAPI.Entities;
using Core.Utilities.Business.Abstract;
using Core.Utilities.Business.Concrete;
using EC.Services.PaymentAPI.Settings;
using ClientSettings = Core.Entities.ClientSettings;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region SERVICES

#region CORS
builder.Services.AddCorsSettings(configuration, Environment);
#endregion
#region MASSTRANSIT
builder.Services.AddMassTransitSettings(configuration);
#endregion
#region AUTOFAC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
#endregion
#region AUTO MAPPER
builder.Services.AddAutoMapper(typeof(MapProfile).Assembly);
#endregion
#region CONTROLLERS
builder.Services.AddControllerSettings();
#endregion
#region REDIS
builder.Services.AddScoped<IRedisCacheManager, RedisCacheManager>();
#endregion
#region ELASTICSEARCH
builder.Services.AddSingleton<IElasticSearchService, ElasticSearchManager>();
builder.Services.AddSingleton<IElasticSearchConfigration, ElasticSearchConfigration>();
builder.Host.UseSerilog();
ElasticSearchExtensions.AddElasticSearch(builder.Services, configuration);
ElasticSearchExtensions.AddELKLogSettings(builder.Services);
#endregion
#region AUTH
builder.Services.AddAuth(configuration);
#endregion
#region CoreModule
builder.Services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule()
            });
#endregion
#region HTTP
builder.Services.AddHttpClientServices(configuration);
#endregion

builder.Services.AddSeedData(configuration);

builder.Services.Configure<ServiceApiSettings>(configuration.GetSection("ServiceApiSettings"));
builder.Services.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));
builder.Services.Configure<ApiEndpoint>(configuration.GetSection("ApiEndpoint"));
builder.Services.Configure<RabbitMqQueues>(configuration.GetSection("RabbitMqQueues"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion



#region PIPELINES
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region CORS
app.UseCors("payment_cors");
#endregion
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion

