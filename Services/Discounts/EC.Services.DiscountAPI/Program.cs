using Autofac.Extensions.DependencyInjection;
using Autofac;
using EC.Services.DiscountAPI.Extensions;
using EC.Services.DiscountAPI.Mappings;
using EC.Services.DiscountAPI.DependencyResolvers.Autofac;
using Microsoft.Extensions.Options;
using EC.Services.DiscountAPI.Settings;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region SERVICES

#region SETTINGS
builder.Services.AddSettings(configuration);
#endregion
#region AUTO MAPPER
builder.Services.AddAutoMapper(typeof(MapProfile).Assembly);
#endregion
#region AUTOFAC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
#endregion
#region MASSTRANSIT
builder.Services.AddRabbitMqConsumer(configuration);
#endregion
#region SEEDDATA
var sp = builder.Services.BuildServiceProvider();
var discountDatabaseSettings = sp.GetRequiredService<IOptions<DiscountDatabaseSettings>>();
SeedDataExtensions.Configure(discountDatabaseSettings.Value);
SeedDataExtensions.AddSeedData();
#endregion
#region CONTROLLERS
builder.Services.AddControllerSettings();
#endregion
#region ENDPOINTS
builder.Services.AddEndpointsApiExplorer();
#endregion
#region SWAGGER
builder.Services.AddSwaggerGen();
#endregion
#region AUTH
builder.Services.AddAuth(configuration);
#endregion

#endregion

#region PIPELINES
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
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

