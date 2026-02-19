namespace GreenTwin.App.Domain;

public class WaterLevelSensor
{
    public int Id { get; }
    public string Description { get; } = string.Empty;


    // Konfiguracja fizyczna
    public double EmptyDistance { get; } // np. 100 cm (pusta beczka)
    public double FullDistance { get; }  // np. 10 cm (pełna beczka - margines bezpieczeństwa)
    public double TotalCapacity { get; } // np. 120 litrów

    // Stan wyliczony
    public double CurrentDistance { get; private set; }
    public double CurrentLiters { get; private set; }
    public double Percentage { get; private set; }
}