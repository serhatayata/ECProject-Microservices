using EC.Services.DiscountAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

#region SERVICES

#region AUTH
builder.Services.AddAuth(configuration);
#endregion
#region CONTROLLERS
builder.Services.AddControllerSettings();
#endregion
#region ENDPOINTS
builder.Services.AddEndpointsApiExplorer();
#endregion
#region SWAGGER
builder.Services.AddSwaggerGen();
#endregion

#endregion

#region PIPELINES
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

