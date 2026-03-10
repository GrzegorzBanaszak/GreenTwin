namespace GreenTwin.App.Dtos;

public class SoilMoistureSensorDto
{
    public required string Id { get; set; }
    public required string Description { get; set; }
    public double MoisturePercentage { get; set; }

}