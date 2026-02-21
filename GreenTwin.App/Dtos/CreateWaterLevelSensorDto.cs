namespace GreenTwin.App.Dtos;

public class CreateWaterLevelSensorDto
{
    public required string Description { get; set; }
    public double EmptyDistanceCm { get; set; }
    public double FullDistanceCm { get; set; }
    public double CapacityLiters { get; set; }
}
