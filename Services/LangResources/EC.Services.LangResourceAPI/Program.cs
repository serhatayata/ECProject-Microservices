using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.DependencyResolvers;
using Core.Entities;
using Core.Entities.ElasticSearch.Abstract;
using Core.Entities.ElasticSearch.Concrete;
using Core.Extensions;
using Core.Utilities.IoC;
using EC.Services.LangResourceAPI.DependencyResolvers.Autofac;
using EC.Services.LangResourceAPI.Extensions;
using EC.Services.LangResourceAPI.Mappings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

#region SERVICES

#region CORS
builder.Services.AddCorsSettings(configuration, environment);
#endregion
#region SETTINGS
builder.Services.Configure<SourceOriginSettings>(configuration.GetSection("SourceOriginSettings"));
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
#region DI
builder.Services.AddScoped<IRedisCacheManager, RedisCacheManager>();
#endregion
#region ELASTICSEARCH
builder.Services.AddSingleton<IElasticSearchLogService, ElasticSearchLogManager>();
builder.Services.AddSingleton<IElasticSearchConfiguration, ElasticSearchConfigration>();
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
#region SEED DATA
builder.Services.AddSeedData(configuration);
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ServiceApiSettings>(configuration.GetSection("ServiceApiSettings"));

#endregion

#region PIPELINES
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("langresource_cors");

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion
