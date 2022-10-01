var builder = WebApplication.CreateBuilder(args);

#region SERVICES

#region Controllers
builder.Services.AddControllers();
#endregion
#region ENDPOINTS
builder.Services.AddEndpointsApiExplorer();
#endregion
#region MyRegion
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

