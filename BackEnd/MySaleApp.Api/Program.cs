using MySaleApp.Api.Extensions;
using MySaleApp.Application;
using MySaleApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplication();
builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.ConfigureCorsPolicy(builder.Configuration);
builder.Services.ConfigureJwtPolicy(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("newPolicy");
app.MapControllers();
app.Run();
