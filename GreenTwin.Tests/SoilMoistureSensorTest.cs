using GreenTwin.App.Models;
using FluentAssertions;

namespace GreenTwin.Tests;



public class SoilMoistureSensorTest
{
    [Fact]
    public void GreenhouseStateShouldContainSensor()
    {
        SoilMoistureSensor sensor = new SoilMoistureSensor(1, "test", 1);

        var value = sensor.GetSensorValue();

        value.Should().Be(100.0);
        sensor.AdcChannel.Should().Be(1);

    }
}