using GreenTwin.App.Data;

namespace GreenTwin.App.Models;


public class SoilMoistureSensor
{
    public int Id { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public int AdcChannel { get; private set; } // Do którego wejścia ADS1115 jest podpięty (0-3)

    // Kalibracja: wartości RAW odczytane w powietrzu i w wodzie
    public int RawDryValue { get; private set; } = 20000;
    public int RawWetValue { get; private set; } = 10000;
    public SoilMoistureSensor(int id, string description, int adcChannel)
    {
        GreenhouseState.AddSensorToChannel(adcChannel, RawWetValue);
        Id = id;
        Description = description;
        AdcChannel = adcChannel;
    }

    public void Calibrate(int dryValue, int wetValue)
    {
        RawDryValue = dryValue;
        RawWetValue = wetValue;
    }
    public double GetSensorValue()
    {
        double? rawAdcValue = GreenhouseState.GetSensorValue(AdcChannel);
        if (rawAdcValue == null)
            return 0.0;
        double percentage = 100.0 * (RawDryValue - rawAdcValue.Value) / (RawDryValue - RawWetValue);
        return Math.Clamp(Math.Round(percentage, 1), 0, 100);
    }
}
