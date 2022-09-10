using Autofac.Extensions.DependencyInjection;
using Autofac;
using EC.Services.ProductAPI.Extensions;
using EC.Services.ProductAPI.Settings.Abstract;
using EC.Services.ProductAPI.DependencyResolvers.Autofac;
using EC.Services.ProductAPI.Mappings;
using Autofac.Core;
using EC.Services.ProductAPI.Data.Abstract;
using EC.Services.ProductAPI.Data.Concrete;

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
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Settings
builder.Services.AddSettings(Configuration);
#endregion
#region SeedData
var sp = builder.Services.BuildServiceProvider();
var productDatabaseSettings = sp.GetRequiredService<IProductDatabaseSettings>();
SeedDataExtensions.Configure(productDatabaseSettings);
SeedDataExtensions.AddSeedData();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion