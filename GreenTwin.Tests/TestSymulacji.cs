using GreenTwin.App.Models;
using GreenTwin.App.Services;
using GreenTwin.App.Simulation;

namespace GreenTwin.Tests;

public class TestSymulacji
{
    [Fact]
    public void GetMoisturePercentage_ShouldBePrecise()
    {
        // Arrange
        var simulator = new GreenhouseSimulator();
        var service = new SoilMoistureService(simulator);

        var sensor = new SoilMoistureSensor
        {
            Id = 1,
            AdcChannel = 2,
            RawDryValue = 20000,
            RawWetValue = 10000,
            Description = "Test sensor"

        };
        service.AddSensor(sensor);

        // Act
        // Ręcznie ustawiamy "sprzęt" na wartość odpowiadającą 75% wilgotności
        // (20000 - 12500) / (20000 - 10000) = 0.75
        simulator.ManualUpdateAdc(2, 12500);

        double result = service.GetMoisturePercentage(1);

        // Assert
        Assert.Equal(75.0, result);
    }
}
