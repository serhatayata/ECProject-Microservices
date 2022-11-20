
#region SERVICES
using EC.Gateway.Middlewares;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();
});

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region Authentication
builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
{
    options.Authority = configuration["IdentityServerURL"];
    options.Audience = "resource_gateway";
    options.RequireHttpsMetadata = false;
});
#endregion

builder.Services.AddOcelot();

#endregion


#region PIPELINES
var app = builder.Build();

var ocelotConfig = new OcelotPipelineConfiguration
{
    AuthorizationMiddleware = async (httpContext, next) =>
    {
        await OcelotAuthorizationMiddleware.Authorize(httpContext, next);
    }
};

app.UseOcelot(ocelotConfig);

app.Run();
#endregion

