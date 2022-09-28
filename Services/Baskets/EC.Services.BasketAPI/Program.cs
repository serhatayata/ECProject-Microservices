using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using EC.Services.BasketAPI.DependencyResolvers.Autofac;
using EC.Services.BasketAPI.Extensions;
using EC.Services.BasketAPI.Mappings;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region Services

#region HTTP
builder.Services.AddHttpContextAccessor();
#endregion
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
#region AUTH
builder.Services.AddAuth(Configuration);
#endregion
#region REDIS
builder.Services.AddScoped<IRedisCacheManager, RedisCacheManager>();
#endregion
#region ENDPOINT
builder.Services.AddEndpointsApiExplorer();
#endregion
#region SWAGGER
builder.Services.AddSwaggerGen();
#endregion
#region CoreModule
builder.Services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule()
            });
#endregion

#endregion

#region Pipelines
var app = builder.Build();

#region SWAGGER
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion
#region HTTP
app.UseHttpsRedirection();
#endregion
#region AUTH
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
#endregion
#region CONTROLLERS
app.MapControllers();
#endregion

app.Run();
#endregion

