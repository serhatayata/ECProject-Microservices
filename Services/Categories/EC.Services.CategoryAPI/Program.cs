using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.DependencyResolvers;
using Core.Entities.ElasticSearch.Abstract;
using Core.Entities.ElasticSearch.Concrete;
using Core.Extensions;
using Core.Utilities.Business.Abstract;
using Core.Utilities.Business.Concrete;
using Core.Utilities.IoC;
using EC.Services.CategoryAPI.DependencyResolvers.Autofac;
using EC.Services.CategoryAPI.Extensions;
using EC.Services.CategoryAPI.Mappings;
using Serilog;
using System.Configuration;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region Services

#region CORS
builder.Services.AddCorsSettings(configuration, Environment);
#endregion
#region AUTOFAC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
#endregion
#region DI
//builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
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
#region ENDPOINT
builder.Services.AddEndpointsApiExplorer();
#endregion
#region SWAGGER
builder.Services.AddSwaggerGen();
#endregion
#region ElasticSearch
builder.Services.AddSingleton<IElasticSearchLogService, ElasticSearchLogManager>();
builder.Services.AddSingleton<IElasticSearchConfiguration, ElasticSearchConfigration>();
builder.Host.UseSerilog();
ElasticSearchExtensions.AddElasticSearch(builder.Services, configuration);
ElasticSearchExtensions.AddELKLogSettings(builder.Services);
#endregion
#region CORE MODULE
builder.Services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule()
            });
#endregion
#region AuthExtensions
builder.Services.AddAuth(configuration);
#endregion
#region SeedData
builder.Services.AddSeedData(configuration);
#endregion

#endregion

#region Pipelines
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region CORS
app.UseCors("category_cors");
#endregion
#region StaticFiles
app.UseStaticFiles();
#endregion
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
#endregion
