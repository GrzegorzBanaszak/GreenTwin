using System.ComponentModel.DataAnnotations;

namespace GreenTwin.App.Dtos;

public class UpdateSoilMoistureSensorDto
{
    [Required] public string Description { get; set; } = string.Empty;
    [Required] public int DryValue { get; set; }
    [Required] public int WetValue { get; set; }
    [Required] public double MinThresholdPercentage { get; set; }
}