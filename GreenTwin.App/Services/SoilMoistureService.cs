using GreenTwin.App.Abstractions;
using GreenTwin.App.Models;

namespace GreenTwin.App.Services;

public class SoilMoistureService : ISoilMoistureService
{
    private readonly IGreenhouseHardware _hardware;
    private readonly List<SoilMoistureSensor> _sensors = new();

    public SoilMoistureService(IGreenhouseHardware hardware)
    {
        _hardware = hardware;
    }

    public void AddSensor(SoilMoistureSensor sensor)
    {
        if (string.IsNullOrWhiteSpace(sensor.Description))
            throw new ArgumentException("Opis czujnika nie może być pusty.");

        if (_sensors.Any(s => s.Id == sensor.Id))
            throw new ArgumentException($"Czujnik o ID {sensor.Id} już istnieje.");

        _sensors.Add(sensor);
    }

    public IEnumerable<SoilMoistureSensor> GetAllSensors() => _sensors.AsReadOnly();

    public SoilMoistureSensor? GetSensorById(int id) => _sensors.FirstOrDefault(s => s.Id == id);

    public double GetMoisturePercentage(int sensorId)
    {
        var sensor = GetSensorById(sensorId)
                     ?? throw new KeyNotFoundException($"Brak czujnika o ID {sensorId}");

        int rawValue = _hardware.ReadRawAdc(sensor.AdcChannel);

        // Formuła mapowania liniowego:
        // percentage = 100 * (Dry - Current) / (Dry - Wet)
        double percentage = 100.0 * (sensor.RawDryValue - rawValue) / (sensor.RawDryValue - sensor.RawWetValue);

        return Math.Clamp(Math.Round(percentage, 1), 0, 100);
    }
}