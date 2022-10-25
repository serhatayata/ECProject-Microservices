using EC.Services.OrderAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region Services

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
