using System;

namespace GreenTwin.App.Abstractions;

public interface IGreenhouseHardware
{
    // Sensory
    double ReadTemperature();
    double ReadHumidity();
    double ReadWaterLevelLiters();
    double ReadSoilMoisture(int sensorId);

    // Wykonawstwo
    void SetPumpState(bool isOn);
    void SetValveState(int valveId, bool isOpen);
    int ReadRawAdc(int channel);
}
