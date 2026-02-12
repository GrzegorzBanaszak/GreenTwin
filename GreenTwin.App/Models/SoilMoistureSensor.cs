namespace GreenTwin.App.Models;


public class SoilMoistureSensor
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int AdcChannel { get; set; } // Do którego wejścia ADS1115 jest podpięty (0-3)

    // Kalibracja: wartości RAW odczytane w powietrzu i w wodzie
    public int RawDryValue { get; set; } = 20000;
    public int RawWetValue { get; set; } = 10000;
}