namespace GreenTwin.App.Domain;

public class LightSensor
{
    public int Id { get; }
    public string Description { get; } = string.Empty;

    // Konfiguracja fizyczna
    public int I2cAddress { get; }

    // Stan
    public double CurrentLux { get; private set; }
    public DateTime LastUpdate { get; private set; }

    // Konfiguracja progów (zależna od gatunku roślin)
    public double MinLuxThreshold { get; set; }
    public double MaxLuxThreshold { get; set; }
}