using System.ComponentModel.DataAnnotations;

namespace GreenTwin.App.Dtos;

public class UpdateSoilMoistureSensorDto
{
    public string Description { get; set; } = string.Empty;
    public double DryValue { get; set; }
    public double WetValue { get; set; }
    public double MinThresholdPercentage { get; set; }
}