using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.Entities.ElasticSearch.Abstract;
using Core.Entities.ElasticSearch.Concrete;
using Core.Extensions;
using EC.Services.Communications.DependencyResolvers.Autofac;
using EC.Services.Communications.Extensions;
using EC.Services.Communications.Models.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

#region SERVICES

#region CORS
builder.Services.AddCors();
#endregion
#region AUTOFAC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
#endregion
#region Controller
builder.Services.AddControllerSettings();
builder.Services.AddEndpointsApiExplorer();
#endregion
#region Swagger
builder.Services.AddSwaggerGen();
#endregion
#region ELK
builder.Services.AddSingleton<IElasticSearchLogService, ElasticSearchLogManager>();
builder.Services.AddSingleton<IElasticSearchConfiguration, ElasticSearchConfigration>();
builder.Host.UseSerilog();
ElasticSearchExtensions.AddElasticSearch(builder.Services, configuration);
ElasticSearchExtensions.AddELKLogSettings(builder.Services);
#endregion
#region DI
builder.Services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
#endregion
#region AUTH
builder.Services.AddAuth(configuration);
#endregion
#region SETTINGS
builder.Services.AddSettings(configuration);
#endregion

#endregion

#region PIPELINE
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("communication_cors");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
#endregion