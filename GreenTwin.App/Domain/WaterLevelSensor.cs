using GreenTwin.App.Data;

namespace GreenTwin.App.Domain;

/// <summary>
/// Reprezentuje ultradźwiękowy czujnik poziomu wody.
/// Mierzy odległość do lustra wody, a następnie przelicza ją na objętość i procent napełnienia.
/// </summary>
public class WaterLevelSensor
{
    // --- Tożsamość i konfiguracja ---
    public int Id { get; }
    public string Description { get; private set; }

    // --- Kalibracja ---
    /// <summary>
    /// Odległość w cm, gdy zbiornik jest pusty. Zazwyczaj największa wartość.
    /// </summary>
    public double EmptyDistanceCm { get; private set; }

    /// <summary>
    /// Odległość w cm, gdy zbiornik jest pełny. Zazwyczaj najniższa wartość.
    /// </summary>
    public double FullDistanceCm { get; private set; }

    /// <summary>
    /// Całkowita pojemność zbiornika w litrach.
    /// </summary>
    public double CapacityLiters { get; private set; }


    // --- Stan ---
    public double LastDistanceReadingCm { get; private set; }
    public double WaterLevelPercentage { get; private set; }
    public double WaterVolumeLiters { get; private set; }
    public DateTime LastUpdate { get; private set; }

    public WaterLevelSensor(int id, string description, double emptyDistanceCm, double fullDistanceCm, double capacityLiters)
    {
        if (emptyDistanceCm <= fullDistanceCm)
        {
            throw new ArgumentException("Wartość dla pustego stanu (EmptyDistanceCm) musi być większa niż dla pełnego (FullDistanceCm).", nameof(emptyDistanceCm));
        }

        Id = id;
        Description = string.IsNullOrWhiteSpace(description) ? $"Czujnik poziomu wody #{id}" : description;
        EmptyDistanceCm = emptyDistanceCm;
        FullDistanceCm = fullDistanceCm;
        CapacityLiters = capacityLiters;
    }

    /// <summary>
    /// Aktualizuje stan czujnika na podstawie nowego surowego odczytu.
    /// </summary>
    public void UpdateWaterLevel(double distanceCm)
    {
        LastDistanceReadingCm = distanceCm;

        // Ograniczamy wartość do skalibrowanego zakresu
        double clampedDistance = Math.Clamp(distanceCm, FullDistanceCm, EmptyDistanceCm);

        // Przeliczamy odległość na procent napełnienia
        // Formuła mapuje zakres [FullDistanceCm, EmptyDistanceCm] na [100%, 0%].
        double range = EmptyDistanceCm - FullDistanceCm;
        WaterLevelPercentage = ((EmptyDistanceCm - clampedDistance) / range) * 100.0;
        WaterVolumeLiters = (WaterLevelPercentage / 100.0) * CapacityLiters;

        LastUpdate = DateTime.UtcNow;
    }

    public void UpdateDescription(string description)
    {
        Description = string.IsNullOrWhiteSpace(description) ? $"Czujnik poziomu wody #{Id}" : description;
    }

    public void UpdateCalibration(double emptyDistanceCm, double fullDistanceCm, double capacityLiters)
    {
        if (emptyDistanceCm <= fullDistanceCm)
        {
            throw new ArgumentException("Wartość dla pustego stanu (EmptyDistanceCm) musi być większa niż dla pełnego (FullDistanceCm).", nameof(emptyDistanceCm));
        }

        EmptyDistanceCm = emptyDistanceCm;
        FullDistanceCm = fullDistanceCm;
        CapacityLiters = capacityLiters;
    }
}
