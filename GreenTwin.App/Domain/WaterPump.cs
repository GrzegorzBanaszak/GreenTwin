namespace GreenTwin.App.Domain;

public class WaterPump
{
    public int Id { get; }
    public string Description { get; } = string.Empty;

    // Konfiguracja fizyczna
    public int RelayPin { get; }
    public double FlowRate { get; } // Litry na minutę (np. 4.3 dla Twojej Seaflo)

    // Stan
    public bool IsRunning { get; private set; }
    public DateTime? LastStartedAt { get; private set; }
    public TimeSpan TotalRunTime { get; private set; } = TimeSpan.Zero;
}
