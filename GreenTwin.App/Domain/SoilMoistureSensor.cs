using GreenTwin.App.Data;

namespace GreenTwin.App.Domain;

/// <summary>
/// Reprezentuje pojemnościowy czujnik wilgotności gleby podłączony przez przetwornik ADC.
/// Klasa hermetyzuje logikę kalibracji i konwersji surowych wartości na procenty.
/// </summary>
public class SoilMoistureSensor
{
    // --- Tożsamość i konfiguracja ---
    public int Id { get; }
    public string Description { get; private set; }
    public int AdcChannel { get; }

    // --- Kalibracja ---
    /// <summary>
    /// Surowa wartość odczytu z ADC, gdy czujnik jest suchy (w powietrzu).
    /// Zazwyczaj jest to najwyższa wartość.
    /// </summary>
    public int DryValue { get; private set; }
    /// <summary>
    /// Surowa wartość odczytu z ADC, gdy czujnik jest w pełni zanurzony w wodzie.
    /// Zazwyczaj jest to najniższa wartość.
    /// </summary>
    public int WetValue { get; private set; }

    // --- Konfiguracja logiki biznesowej ---
    /// <summary>
    /// Próg wilgotności (w procentach), poniżej którego należy podjąć akcję (np. podlewanie).
    /// </summary>
    public double MinThresholdPercentage { get; set; }

    // --- Stan ---
    public int LastRawReading { get; private set; }
    public double MoisturePercentage { get; private set; }
    public DateTime LastUpdate { get; private set; }

    public SoilMoistureSensor(int id, string description, int adcChannel, int dryValue, int wetValue)
    {
        if (dryValue <= wetValue)
        {
            throw new ArgumentException("Wartość dla suchego stanu (DryValue) musi być większa niż dla mokrego (WetValue).", nameof(dryValue));
        }

        Id = id;
        Description = string.IsNullOrWhiteSpace(description) ? $"Czujnik gleby #{id}" : description;
        AdcChannel = adcChannel;
        DryValue = dryValue;
        WetValue = wetValue;
    }

    /// <summary>
    /// Aktualizuje stan czujnika na podstawie nowego surowego odczytu z ADC.
    /// W trybie symulacji dane pobierane są z `GreenhouseState`.
    /// </summary>
    public void UpdateMoisture()
    {
        // W trybie symulacji, czujnik "odpytuje" cyfrowego bliźniaka o swój stan.
        // W trybie fizycznym, ta linia zostanie zastąpiona przez odczyt z dedykowanego providera sprzętowego.
        int rawAdcValue = GreenhouseState.GetAdcRawValue(AdcChannel);

        LastRawReading = rawAdcValue;

        // Ograniczamy wartość do skalibrowanego zakresu, aby uniknąć procentów > 100 lub < 0.
        int clampedRawValue = Math.Clamp(rawAdcValue, WetValue, DryValue);

        // Przeliczamy wartość na procenty. Wyższa wartość surowa oznacza bardziej suchą glebę.
        // Formuła mapuje zakres [WetValue, DryValue] na [100%, 0%].
        double range = DryValue - WetValue;
        MoisturePercentage = ((double)(DryValue - clampedRawValue) / range) * 100.0;

        LastUpdate = DateTime.UtcNow;
    }

    /// <summary>
    /// Aktualizuje opis czujnika.
    /// </summary>
    public void UpdateDescription(string description)
    {
        Description = string.IsNullOrWhiteSpace(description) ? $"Czujnik gleby #{Id}" : description;
    }

    /// <summary>
    /// Aktualizuje wartości kalibracyjne czujnika.
    /// </summary>
    public void UpdateCalibration(int dryValue, int wetValue)
    {
        if (dryValue <= wetValue)
        {
            throw new ArgumentException("Wartość dla suchego stanu (DryValue) musi być większa niż dla mokrego (WetValue).", nameof(dryValue));
        }

        DryValue = dryValue;
        WetValue = wetValue;
    }
}