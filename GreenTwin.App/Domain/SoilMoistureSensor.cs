using GreenTwin.App.Data;

namespace GreenTwin.App.Domain;

public class SoilMoistureSensor
{

    public int Id { get; }
    public string Description { get; private set; }
    public int AdcChannel { get; }
    private double DryValue { get; set; }
    private double WetValue { get; set; }
    public double MinThresholdPercentage { get; set; }

    // --- Stan ---
    public double MoisturePercentage { get; private set; }

    public SoilMoistureSensor(int id, string description, int adcChannel, double? dryValue, double? wetValue)
    {
        if (dryValue <= wetValue)
        {
            throw new ArgumentException("Wartość dla suchego stanu (DryValue) musi być większa niż dla mokrego (WetValue).", nameof(dryValue));
        }

        Id = id;
        Description = string.IsNullOrWhiteSpace(description) ? $"Czujnik gleby #{id}" : description;
        AdcChannel = adcChannel;
        DryValue = dryValue ?? 2.407;
        WetValue = wetValue ?? 1.125;
    }


    public void UpdateMoisture()
    {

        double currentVoltage = GreenhouseState.GetAdcRawValue(AdcChannel);
        double moisturePercent = 100 - ((currentVoltage - WetValue) / (DryValue - WetValue) * 100);
        MoisturePercentage = Math.Round(Math.Clamp(moisturePercent, 0, 100), MidpointRounding.AwayFromZero);
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
    public void UpdateCalibration(int? dryValue, int? wetValue)
    {
        if (dryValue <= wetValue)
        {
            throw new ArgumentException("Wartość dla suchego stanu (DryValue) musi być większa niż dla mokrego (WetValue).", nameof(dryValue));
        }

        DryValue = dryValue ?? DryValue;
        WetValue = wetValue ?? WetValue;
    }
}