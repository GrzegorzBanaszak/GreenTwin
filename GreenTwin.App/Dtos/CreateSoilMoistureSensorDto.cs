using System.ComponentModel.DataAnnotations;

namespace GreenTwin.App.Dtos;

public class CreateSoilMoistureSensorDto
{
    public string Description { get; set; } = string.Empty;
    [Range(0, 3)] public int AdcChannel { get; set; }
    public double? DryValue { get; set; }
    public double? WetValue { get; set; }
}