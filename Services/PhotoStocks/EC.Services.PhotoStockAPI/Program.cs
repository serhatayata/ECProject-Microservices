using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using EC.Services.PhotoStockAPI.DependencyResolvers.Autofac;
using EC.Services.PhotoStockAPI.Extensions;
using EC.Services.PhotoStockAPI.Mappings;

var builder = WebApplication.CreateBuilder(args);
Microsoft.Extensions.Configuration.ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region Services

#region AUTOFAC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
#endregion
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region AUTO MAPPER
builder.Services.AddAutoMapper(typeof(MapProfile).Assembly);
#endregion
#region AuthExtensions
builder.Services.AddAuth(configuration);
#endregion
#region SEED DATA
builder.Services.AddSeedData(configuration);
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
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion

