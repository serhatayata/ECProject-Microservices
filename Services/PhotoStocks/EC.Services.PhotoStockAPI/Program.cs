using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.DependencyResolvers;
using Core.Entities.ElasticSearch.Abstract;
using Core.Entities.ElasticSearch.Concrete;
using Core.Extensions;
using Core.Utilities.Helpers;
using Core.Utilities.IoC;
using EC.Services.PhotoStockAPI.DependencyResolvers.Autofac;
using EC.Services.PhotoStockAPI.Extensions;
using EC.Services.PhotoStockAPI.Mappings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Microsoft.Extensions.Configuration.ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region Services
#region AUTOFAC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
#endregion
#region CONTROLLER
builder.Services.AddControllerSettings();
builder.Services.AddEndpointsApiExplorer();
#endregion
#region SWAGGER
builder.Services.AddSwaggerGen();
#endregion
#region AUTO MAPPER
builder.Services.AddAutoMapper(typeof(MapProfile).Assembly);
#endregion
#region CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:6000",
                                "http://localhost:6001",
                                "http://localhost:6002",
                                "http://localhost:6003",
                                "http://localhost:6004",
                                "http://localhost:6005",
                                "http://localhost:6006",
                                "http://localhost:6007"
                                )
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});
#endregion
#region AuthExtensions
builder.Services.AddAuth(configuration);
#endregion
#region SEED DATA
builder.Services.AddSeedData(configuration);
#endregion
#region ElasticSearch
builder.Services.AddSingleton<IElasticSearchService, ElasticSearchManager>();
builder.Services.AddSingleton<IElasticSearchConfigration, ElasticSearchConfigration>();
builder.Host.UseSerilog();
ElasticSearchExtensions.AddElasticSearch(builder.Services,configuration);
ElasticSearchExtensions.AddELKLogSettings(builder.Services);
#endregion
#region CoreModule
builder.Services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule()
            });
#endregion

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Pipelines

#region HTTP
HttpContextHelper.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());
app.UseHttpsRedirection();
#endregion
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();

app.Run();
#endregion

