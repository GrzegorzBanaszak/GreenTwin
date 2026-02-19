

using GreenTwin.App.Application.Interfaces;
using GreenTwin.App.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. CORS - upewnij się, że porty się zgadzają z Live Serverem!
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// 2. Rejestracja usług
builder.Services.AddSignalR(
    options =>
    {
        options.EnableDetailedErrors = true;
    }
);

// Rejestracja serwisu aplikacyjnego jako singleton, ponieważ przechowuje stan w pamięci.
builder.Services.AddSingleton<ISoilMoistureSensorService, SoilMoistureSensorService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();