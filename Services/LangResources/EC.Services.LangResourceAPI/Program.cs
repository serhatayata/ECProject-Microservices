using Autofac.Extensions.DependencyInjection;
using Autofac;
using Core.Entities;
using Core.Extensions;
using Serilog;
using Core.Entities.ElasticSearch.Abstract;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.Entities.ElasticSearch.Concrete;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.DependencyResolvers;
using Core.Utilities.IoC;
using EC.Services.LangResourceAPI.Extensions;
using EC.Services.LangResourceAPI.DependencyResolvers.Autofac;
using EC.Services.LangResourceAPI.Mappings;
using EC.Services.LangResourceAPI.Services.Abstract;
using EC.Services.LangResourceAPI.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region SERVICES

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

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion
