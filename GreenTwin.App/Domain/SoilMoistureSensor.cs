namespace GreenTwin.App.Domain;

public class SoilMoistureSensor
{
    public int Id { get; }
    public string Description { get; } = string.Empty;

    public int AdcChannel { get; }

    // Kalibracja
    public int DryValue { get; private set; }
    public int WetValue { get; private set; }

    // Progi biznesowe
    public double MinThreshold { get; set; }

    // Stan
    public int LastRawReading { get; private set; }
    public double MoisturePercentage { get; private set; }
}