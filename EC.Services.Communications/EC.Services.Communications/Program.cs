using Autofac.Core;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.Entities.ElasticSearch.Abstract;
using Core.Entities.ElasticSearch.Concrete;
using Core.Extensions;
using EC.Services.Communications.Extensions;
using EC.Services.Communications.Models.Settings;
using EC.Services.Communications.Services.Abstract;
using EC.Services.Communications.Services.Concrete;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

#region SERVICES

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

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddSingleton<IElasticSearchLogService, ElasticSearchLogManager>();
#endregion
#region Auth
builder.Services.AddAuth(configuration);
#endregion

#endregion

#region PIPELINE
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
#endregion