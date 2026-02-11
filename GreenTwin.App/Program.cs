using GreenTwin.App.Abstractions;
using GreenTwin.App.Simulation;
using GreenTwin.App.Hubs;
using GreenTwin.App.Services; // Tu zaraz stworzymy klasę

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
builder.Services.AddSingleton<IGreenhouseHardware, GreenhouseSimulator>();
builder.Services.AddSignalR(
    options =>
    {
        options.EnableDetailedErrors = true;
    }
);

// 3. Dodanie logiki działającej w tle (nasza szklarnia)
builder.Services.AddHostedService<GreenhouseLogicService>();

var app = builder.Build();

app.UseCors("AllowAll");

app.MapHub<GreenhouseHub>("/greenhouseHub");

app.Run();