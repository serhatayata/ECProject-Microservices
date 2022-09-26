using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using EC.Services.ProductAPI.DependencyResolvers.Autofac;
using EC.Services.ProductAPI.Extensions;
using EC.Services.ProductAPI.Mappings;
using EC.Services.ProductAPI.Settings.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region Services

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AUTH
builder.Services.AddAuth(Configuration);
#endregion
#region SETTINGS
builder.Services.AddSettings(Configuration);
#endregion
#region SEEDDATA
var sp = builder.Services.BuildServiceProvider();
var productDatabaseSettings = sp.GetRequiredService<IProductDatabaseSettings>();
SeedDataExtensions.Configure(productDatabaseSettings);
SeedDataExtensions.AddSeedData();
#endregion
#region CoreModule
builder.Services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule()
            });
#endregion
var app = builder.Build();
#endregion

#region Pipeline
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