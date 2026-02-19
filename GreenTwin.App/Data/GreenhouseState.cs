namespace GreenTwin.App.Data;


public static class GreenhouseState
{
    public static double WaterDistanceCm { get; set; } = 50.0;
    public static double Temperature { get; set; } = 22.5;
    public static double Humidity { get; set; } = 50.0;

    // Słownik dla wartości RAW ADC (kanał -> wartość)
    private static List<AdcSensorData> AdcChannels { get; set; } = new();

    public static void AddSensorToChannel(int adcChannel, int value)
    {
        AdcChannels.Add(new AdcSensorData(adcChannel, value));
    }

    public static double? GetSensorValue(int adcChannel)
    {
        var sensor = AdcChannels.FirstOrDefault(x => x.Channel == adcChannel);
        if (sensor == null)
            return null;
        return sensor.Value;
    }


    public static bool IsPumpOn { get; set; }
    public static Dictionary<int, bool> ValveStates { get; set; } = new();

    public static void ClearState()
    {
        WaterDistanceCm = 50.0;
        Temperature = 22.5;
        Humidity = 50.0;
        AdcChannels.Clear();
        IsPumpOn = false;
        ValveStates.Clear();

    }
}