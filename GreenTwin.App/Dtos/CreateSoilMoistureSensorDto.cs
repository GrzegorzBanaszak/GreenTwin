using System.ComponentModel.DataAnnotations;

namespace GreenTwin.App.Dtos;

public class CreateSoilMoistureSensorDto
{
    [Required] public string Description { get; set; } = string.Empty;
    [Range(0, 3)] public int AdcChannel { get; set; }
    [Required] public int DryValue { get; set; }
    [Required] public int WetValue { get; set; }
}