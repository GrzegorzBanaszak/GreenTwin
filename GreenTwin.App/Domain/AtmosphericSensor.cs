namespace GreenTwin.App.Domain;


public class AtmosphericSensor
{
    public int Id { get; }
    public string Description { get; } = string.Empty;

    // Konfiguracja fizyczna
    public int I2cAddress { get; }

    // Stan odczytów
    public double Temperature { get; private set; }
    public double Humidity { get; private set; }
    public double Pressure { get; private set; }
    public DateTime LastUpdate { get; private set; }
}