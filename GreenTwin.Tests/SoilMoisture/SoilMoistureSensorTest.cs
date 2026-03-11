using FluentAssertions;
using GreenTwin.App.Data;
using GreenTwin.App.Domain;

namespace GreenTwin.Tests.SoilMoisture;

public class SoilMoistureSensorTest : IDisposable
{
    public void Dispose()
    {
        GreenhouseState.ClearState();
        // GreenhouseState.SeedAdcSensorData();
    }

    [Fact]
    public void SoilMoistureSensorShouldWork()
    {
        double dryValue = 2.407;
        double wetValue = 1.125;
        SoilMoistureSensor sensor = new SoilMoistureSensor(1, "Pomidory", 0, dryValue, wetValue);
        GreenhouseState.AddAdcRawValue(0, 1.950);
        GreenhouseState.AdcRawValues.FirstOrDefault(x => x.Channel == 0)!.Value = 1.950;

        sensor.UpdateMoisture();

        sensor.AdcChannel.Should().Be(0);
        sensor.Description.Should().Be("Pomidory");
        sensor.MoisturePercentage.Should().NotBe(null);
    }

    [Fact]
    public void SoilMoistureSensor_Constructor_WithoutCalibration_Should_SetDefaultValues()
    {
        SoilMoistureSensor sensor = new SoilMoistureSensor(1, "Pomidory", 0, null, null);

        sensor.Id.Should().Be(1);
        sensor.Description.Should().Be("Pomidory");
        sensor.AdcChannel.Should().Be(0);
        sensor.DryValue.Should().Be(2.407);
        sensor.WetValue.Should().Be(1.125);
    }

    [Fact]
    public void SoilMoistureSensor_UpdateMoisture_Should_SetCorrectMoisturePercentage()
    {
        double dryValue = 2.407;
        double wetValue = 1.125;
        SoilMoistureSensor sensor = new SoilMoistureSensor(1, "Pomidory", 0, dryValue, wetValue);
        GreenhouseState.AddAdcRawValue(0, 1.950);
        GreenhouseState.AdcRawValues.FirstOrDefault(x => x.Channel == 0)!.Value = 1.950;

        sensor.UpdateMoisture();

        sensor.MoisturePercentage.Should().NotBe(null);
        sensor.MoisturePercentage.Should().BeGreaterThanOrEqualTo(0);
        sensor.MoisturePercentage.Should().BeLessThanOrEqualTo(100);
    }

    [Fact]
    public void SoilMoistureSensor_UpdateDescription_Should_SetCorrectDescription()
    {
        SoilMoistureSensor sensor = new SoilMoistureSensor(1, "Pomidory", 0, 2.407, 1.125);

        sensor.UpdateDescription("Nowy opis");

        sensor.Description.Should().Be("Nowy opis");
    }

    [Fact]
    public void SoilMoistureSensor_UpdateCalibration_Should_SetCorrectCalibrationValues()
    {
        SoilMoistureSensor sensor = new SoilMoistureSensor(1, "Pomidory", 0, 2.407, 1.125);

        sensor.UpdateCalibration(3.14, 0.5);

        sensor.DryValue.Should().Be(3.14);
        sensor.WetValue.Should().Be(0.5);
    }
}