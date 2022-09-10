using EC.Services.ProductAPI.Extensions;
using EC.Services.ProductAPI.Settings.Abstract;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Settings
builder.Services.AddSettings(Configuration);
#endregion
#region SeedData
var sp = builder.Services.BuildServiceProvider();
var productDatabaseSettings = sp.GetRequiredService<IProductDatabaseSettings>();
SeedDataExtensions.Configure(productDatabaseSettings);
SeedDataExtensions.AddSeedData();
#endregion
var app = builder.Build();
#endregion

#region Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion