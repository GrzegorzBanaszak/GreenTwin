using GreenTwin.App.Abstractions;
using GreenTwin.App.Dto;
using GreenTwin.App.Models;

namespace GreenTwin.App.Services;

public class SoilMoistureService : ISoilMoistureService
{
    private List<SoilMoistureSensor> _sensors = new List<SoilMoistureSensor>();

    public void AddSensor(CreateSoilMoistureSensor sensor)
    {
        SoilMoistureSensor newSensor = new SoilMoistureSensor(sensor.Id, sensor.Description, sensor.AdcChannel);
        _sensors.Add(newSensor);
    }

    public IEnumerable<SoilMoistureSensor> GetAllSensors() => _sensors.AsReadOnly();
    public SoilMoistureSensor? GetSensorById(int id)
    {
        var sensor = _sensors.FirstOrDefault(s => s.Id == id);
        if (sensor == null)
            return null;
        return sensor;
    }

    public double? GetSensorValue(int sensorId)
    {
        var sensor = GetSensorById(sensorId);
        if (sensor == null)
            return null;
        return sensor.GetSensorValue();
    }

    public bool RemoveSensor(int id)
    {
        int removedCount = _sensors.RemoveAll(s => s.Id == id);
        return removedCount > 0;
    }
}