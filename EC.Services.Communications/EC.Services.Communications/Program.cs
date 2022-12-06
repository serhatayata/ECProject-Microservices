using Autofac.Core;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.Entities.ElasticSearch.Abstract;
using Core.Entities.ElasticSearch.Concrete;
using Core.Extensions;
using EC.Services.Communications.Models.Settings;
using EC.Services.Communications.Services.Abstract;
using EC.Services.Communications.Services.Concrete;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

#region SERVICES
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region ELK
builder.Services.AddSingleton<IElasticSearchLogService, ElasticSearchLogManager>();
builder.Services.AddSingleton<IElasticSearchConfiguration, ElasticSearchConfigration>();
builder.Host.UseSerilog();
ElasticSearchExtensions.AddElasticSearch(builder.Services, configuration);
ElasticSearchExtensions.AddELKLogSettings(builder.Services);
#endregion
builder.Services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddSingleton<IElasticSearchLogService, ElasticSearchLogManager>();
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