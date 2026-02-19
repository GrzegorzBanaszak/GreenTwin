namespace GreenTwin.App.Domain;


public class WaterValve
{
    public int Id { get; }
    public string Description { get; } = string.Empty;

    // Konfiguracja fizyczna
    public int RelayPin { get; }

    // Stan
    public bool IsOpen { get; private set; }
    public DateTime? LastOpenedAt { get; private set; }
    public bool ManualOverride { get; private set; }
}