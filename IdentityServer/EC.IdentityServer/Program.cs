using Autofac.Core;
using EC.IdentityServer.Data.DbContext;
using EC.IdentityServer.Data.SeedData;
using EC.IdentityServer.Extensions;
using EC.IdentityServer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

var assembly = typeof(Program).Assembly.GetName().Name;

#region Services




#region IdentityDbContext Settings
var identityConnString = Configuration.GetConnectionString("IdentityConnection");
builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"), b => b.MigrationsAssembly(assembly)));
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

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;

    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
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

IdentityExtensions.AddDbContextSettings(identityConnString, Configuration);
#endregion
#region SeedData
await AppIdentityDbContextSeed.AddUserSeedDataAsync();
await ConfigurationDbContextSeed.AddIdentityConfigurationSeedData(Configuration);
#endregion
#region Logging
builder.Services.AddLogging();
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
app.UseRouting();
app.UseHttpsRedirection();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion

