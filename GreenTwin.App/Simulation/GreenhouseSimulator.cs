using System;
using GreenTwin.App.Abstractions;

namespace GreenTwin.App.Simulation;

public class GreenhouseSimulator : IGreenhouseHardware
{
    private bool _pumpOn;
    private double _currentWaterInLiters = 120.0;

    public double ReadTemperature() => 22.5; // Stała wartość dla testów
    public double ReadHumidity() => 60.0;
    public double ReadWaterLevelLiters()
    {
        if (_pumpOn) _currentWaterInLiters -= 0.1;
        return _currentWaterInLiters;
    }
    public double ReadSoilMoisture(int sensorId) => 45.0;

    public void SetPumpState(bool isOn)
    {
        _pumpOn = isOn;
        Console.WriteLine($"[SIMULATOR] Pompa: {(isOn ? "WŁĄCZONA" : "WYŁĄCZONA")}");
    }
    public void SetValveState(int valveId, bool isOpen)
    {
        Console.WriteLine($"[SIMULATOR] Zawór {valveId}: {(isOpen ? "OTWARTY" : "ZAMKNIĘTY")}");
    }
}
