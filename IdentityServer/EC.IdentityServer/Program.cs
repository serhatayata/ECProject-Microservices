using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using EC.IdentityServer.Data.DbContext;
using EC.IdentityServer.Data.SeedData;
using EC.IdentityServer.DependencyResolvers.Autofac;
using EC.IdentityServer.Extensions;
using EC.IdentityServer.Mappings;
using EC.IdentityServer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

var assembly = typeof(Program).Assembly.GetName().Name;
var identityConnString = Configuration.GetConnectionString("IdentityConnection");

#region Services

#region AUTOFAC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
#endregion
#region AUTO MAPPER
builder.Services.AddAutoMapper(typeof(MapProfile).Assembly);
#endregion
#region IDENTITY - DBCONTEXT
builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(identityConnString, b => b.MigrationsAssembly(assembly)));

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    //this place might be changed...
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddLocalApiAuthentication();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;

    //see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
    options.EmitStaticAudienceClaim = true;
})
  .AddAspNetIdentity<AppUser>()
  .AddConfigurationStore(options =>
  {
      options.ConfigureDbContext = b => b.UseSqlServer(identityConnString, opt => opt.MigrationsAssembly(assembly));
  }).AddOperationalStore(options =>
  {
      options.ConfigureDbContext = b =>
                  b.UseSqlServer(identityConnString, opt => opt.MigrationsAssembly(assembly));
  })
    .AddDeveloperSigningCredential(); //Sertifika yoksa
#endregion
#region HTTP CONTEXT ACCESSOR
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#endregion
#region IOPTIONS
builder.Services.AddOptionsPattern(Configuration);
#endregion
#region SEED_DATA
await AppIdentityDbContextSeed.AddUserSettingsAsync(identityConnString);
await ConfigurationDbContextSeed.AddIdentityConfigurationSettingsAsync(Configuration);
#endregion
#region LOGGING
//builder.Services.AddLogging();
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Pipeline
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseIdentityServer();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion

