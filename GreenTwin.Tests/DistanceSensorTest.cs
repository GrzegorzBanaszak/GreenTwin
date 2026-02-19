using Xunit;
using FluentAssertions;
using GreenTwin.App.Data;
using GreenTwin.App.Models;

namespace GreenTwin.Tests;

public class DistanceSensorTest : IDisposable
{
    public void Dispose()
    {
        GreenhouseState.ClearState();
    }

    [Fact]
    public async Task MeasureLitersAsync_ShouldReturnCorrectValue_WhenEnvironmentIsStable()
    {
        // Arrange
        var sensor = new DistanceSensor(1, "Test Sensor", 23, 24);
        // Ustawiamy poziom wody w symulacji na 50 cm (połowa zbiornika 100cm)
        GreenhouseState.WaterDistanceCm = 50.0;

        // Act
        var result = await sensor.MeasureLitersAsync();

        // Assert
        // Oczekujemy ok. 60L (połowa z 120L) +/- szum pomiarowy
        // Szum to +/- 0.5 cm, co przekłada się na ok. +/- 0.6L
        result.Should().NotBeNull();
        result.Value.Should().BeInRange(59.0, 61.0);
    }

    [Fact]
    public async Task MeasureLitersAsync_ShouldClampToZero_WhenEmpty()
    {
        // Arrange
        var sensor = new DistanceSensor(1, "Test Sensor", 23, 24);
        // Odległość większa niż wysokość zbiornika
        GreenhouseState.WaterDistanceCm = 110.0;

        // Act
        var result = await sensor.MeasureLitersAsync();

        // Assert
        result.Should().Be(0.0);
    }

    [Fact]
    public async Task MeasureLitersAsync_ShouldClampToMax_WhenFull()
    {
        // Arrange
        var sensor = new DistanceSensor(1, "Test Sensor", 23, 24);
        // Pełny zbiornik (woda przy samym czujniku)
        GreenhouseState.WaterDistanceCm = 0.0;

        // Act
        var result = await sensor.MeasureLitersAsync();

        // Assert
        // Ze względu na szum, wartość może być minimalnie mniejsza niż MAX, 
        // ale nigdy większa (dzięki Math.Clamp)
        result.Should().BeLessThanOrEqualTo(120.0);
        result.Should().BeGreaterThan(118.0);
    }

    [Fact]
    public async Task MeasureLitersAsync_ShouldReturnNull_WhenSimulatingErrors()
    {
        // Arrange
        var sensor = new DistanceSensor(1, "Error Tank", 23, 24)
        {
            SimulateReadErrors = true
        };

        // Act
        // Ponieważ błąd jest losowy (>0.9), wykonujemy wiele prób, 
        // aby upewnić się, że w końcu zwróci null
        List<double?> results = new List<double?>();
        for (int i = 0; i < 100; i++)
        {
            results.Add(await sensor.MeasureLitersAsync());
        }

        // Assert
        results.Should().Contain((double?)null);
    }
}