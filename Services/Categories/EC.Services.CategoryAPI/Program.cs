using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using EC.Services.CategoryAPI.DependencyResolvers.Autofac;
using EC.Services.CategoryAPI.Extensions;
using EC.Services.CategoryAPI.Mappings;

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
builder.Services.AddControllers();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region CORE MODULE
builder.Services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule()
            });
#endregion
#region AuthExtensions
builder.Services.AddAuth(Configuration);
#endregion
#region SeedData
builder.Services.AddSeedData(Configuration);
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
