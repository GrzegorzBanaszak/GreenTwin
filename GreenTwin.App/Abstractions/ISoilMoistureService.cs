using GreenTwin.App.Models;

public interface ISoilMoistureService
{
    void AddSensor(SoilMoistureSensor sensor);
    IEnumerable<SoilMoistureSensor> GetAllSensors();
    SoilMoistureSensor? GetSensorById(int id);
    double GetMoisturePercentage(int sensorId);
}