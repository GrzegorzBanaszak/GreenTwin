using GreenTwin.App.Models;
using FluentAssertions;
using GreenTwin.App.Data;
using GreenTwin.App.Services;
using GreenTwin.App.Dto;

namespace GreenTwin.Tests;



public class SoilMoistureSensorTest : IDisposable
{

    public void Dispose()
    {
        GreenhouseState.ClearState();
    }

    [Fact]
    public void GreenhouseStateShouldContainSensor()
    {
        SoilMoistureSensor sensor = new SoilMoistureSensor(1, "test", 1);

        var value = sensor.GetSensorValue();

        value.Should().Be(100.0);
        sensor.AdcChannel.Should().Be(1);
    }


    [Fact]
    public void GetListOfSensors()
    {
        SoilMoistureService service = new SoilMoistureService();
        service.AddSensor(new CreateSoilMoistureSensor(1, "test", 1));
        service.AddSensor(new CreateSoilMoistureSensor(2, "test2", 2));

        var sensors = service.GetAllSensors();

        sensors.Should().NotBeNull();
        sensors.Should().HaveCount(2);
    }

    [Fact]
    public void GetSensorFromService()
    {
        SoilMoistureService service = new SoilMoistureService();
        service.AddSensor(new CreateSoilMoistureSensor(1, "test", 1));

        var sensor = service.GetSensorById(1);


        sensor.Should().NotBeNull();
        sensor.Description.Should().Be("test");
        sensor.AdcChannel.Should().Be(1);
    }
}