namespace GreenTwin.App.Data;


public class AdcSensorData
{
    public int Channel { get; set; }
    public double Value { get; set; }

    public AdcSensorData(int channel, double value)
    {
        Channel = channel;
        Value = value;
    }
}


