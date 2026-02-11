using GreenTwin.App.Abstractions;

namespace GreenTwin.App.Simulation;

public class GreenhouseSimulator : IGreenhouseHardware
{
    private bool _pumpOn = false;
    private double _currentWaterInLiters = 120.0;

    // Przechowujemy stany wielu zaworów
    private Dictionary<int, bool> _valveStates = new Dictionary<int, bool>();

    public double ReadTemperature() => 24.2;
    public double ReadHumidity() => 55.0;

    public double ReadWaterLevelLiters()
    {
        // LOGIKA SYMULACJI: 
        // Woda ubywa tylko wtedy, gdy pompa jest włączona 
        // ORAZ przynajmniej jeden zawór jest otwarty.
        bool anyValveOpen = _valveStates.Values.Any(state => state == true);

        if (_pumpOn && anyValveOpen)
        {
            // Symulujemy ubytek wody (np. 0.05 litra na każde zapytanie)
            _currentWaterInLiters -= 0.05;
            if (_currentWaterInLiters < 0) _currentWaterInLiters = 0;
        }

        return _currentWaterInLiters;
    }

    public double ReadSoilMoisture(int sensorId) => 30.0;

    public void SetPumpState(bool isOn)
    {
        _pumpOn = isOn;
        Console.WriteLine($"[SIMULATOR] Pompa jest teraz: {(isOn ? "WŁĄCZONA" : "WYŁĄCZONA")}");
    }

    public void SetValveState(int valveId, bool isOpen)
    {
        _valveStates[valveId] = isOpen;
        Console.WriteLine($"[SIMULATOR] Zawór {valveId} jest teraz: {(isOpen ? "OTWARTY" : "ZAMKNIĘTY")}");
    }
}