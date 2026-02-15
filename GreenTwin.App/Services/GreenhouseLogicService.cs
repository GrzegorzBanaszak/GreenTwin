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

    }
}