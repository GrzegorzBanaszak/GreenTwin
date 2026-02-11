using GreenTwin.App.Abstractions;
using Microsoft.AspNetCore.SignalR;
using GreenTwin.App.Hubs;

namespace GreenTwin.App.Services;

public class GreenhouseLogicService : BackgroundService
{
    private readonly IGreenhouseHardware _hardware;
    private readonly IHubContext<GreenhouseHub> _hubContext;

    public GreenhouseLogicService(IGreenhouseHardware hardware, IHubContext<GreenhouseHub> hubContext)
    {
        _hardware = hardware;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var water = _hardware.ReadWaterLevelLiters();

            // Wysyłamy poziom wody do wszystkich aplikacji mobilnych co sekundę
            await _hubContext.Clients.All.SendAsync("WaterLevelUpdated", water, stoppingToken);

            // Logika bezpieczeństwa: jeśli mało wody, wyłączamy pompę
            if (water < 10)
            {
                _hardware.SetPumpState(false);
                await _hubContext.Clients.All.SendAsync("Alert", "KRYTYCZNIE NISKI POZIOM WODY!", stoppingToken);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}