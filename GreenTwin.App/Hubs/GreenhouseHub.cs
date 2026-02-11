using Microsoft.AspNetCore.SignalR;
using GreenTwin.App.Abstractions;

namespace GreenTwin.App.Hubs;

public class GreenhouseHub : Hub
{
    private readonly IGreenhouseHardware _hardware;

    public GreenhouseHub(IGreenhouseHardware hardware)
    {
        _hardware = hardware;
    }

    // Tę metodę wywoła Twoja aplikacja mobilna
    public async Task RequestValveToggle(int valveId, bool state)
    {
        _hardware.SetValveState(valveId, state);

        // Jeśli otwieramy jakikolwiek zawór, włączamy pompę.
        // Jeśli zamykamy, w tym prostym przykładzie też sterujemy pompą.
        _hardware.SetPumpState(state);

        await Clients.All.SendAsync("ValveStatusChanged", valveId, state);
        // Powiadomienie o stanie pompy (opcjonalnie)
        await Clients.All.SendAsync("PumpStatusChanged", state);
    }
}