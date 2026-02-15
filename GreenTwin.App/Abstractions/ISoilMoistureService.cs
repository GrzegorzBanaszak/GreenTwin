using GreenTwin.App.Dto;
using GreenTwin.App.Models;

public interface ISoilMoistureService
{
    IEnumerable<SoilMoistureSensor> GetAllSensors();
    SoilMoistureSensor? GetSensorById(int id);
    void AddSensor(CreateSoilMoistureSensor sensor);
    bool RemoveSensor(int id);
    double? GetSensorValue(int sensorId);
}


