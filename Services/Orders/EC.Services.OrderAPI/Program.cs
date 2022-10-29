using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Core.Entities;
using EC.Services.Order.Application.DependencyResolvers.Autofac;
using EC.Services.Order.Application.Handlers;
using EC.Services.Order.Application.Mapping;
using EC.Services.OrderAPI.Extensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region Services

builder.Services.AddMediatR(typeof(CreateOrderCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetOrdersByUserIdQueryHandler).Assembly);
#region AUTOFAC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
#endregion
#region AUTO MAPPER
builder.Services.AddAutoMapper(typeof(CustomMapping).Assembly);
#endregion
#region CONTROLLERS
builder.Services.AddControllerSettings();
#endregion
#region ENDPOINT
builder.Services.AddEndpointsApiExplorer();
#endregion
#region QUEUE
builder.Services.AddMassTransitSettings(configuration);
#endregion
#region AUTH
builder.Services.AddAuth(configuration);
#endregion
#region SETTINGS
builder.Services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));
#endregion
#region SEED DATA
builder.Services.AddSeedData(configuration);
#endregion

builder.Services.AddSwaggerGen();

#endregion

#region PIPELINES
var app = builder.Build();

// Configure the HTTP request pipeline.
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
