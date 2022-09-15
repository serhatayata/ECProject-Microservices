using EC.Services.CategoryAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region SETTINGS
builder.Services.AddSettings(Configuration);
#endregion
#region AuthExtensions
builder.Services.AddAuth(Configuration);
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion
