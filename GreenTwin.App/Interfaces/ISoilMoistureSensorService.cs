using GreenTwin.App.Domain;

namespace GreenTwin.App.Application.Interfaces;

/// <summary>
/// Definiuje operacje do zarządzania czujnikami wilgotności gleby.
/// </summary>
public interface ISoilMoistureSensorService
{
    Task<IEnumerable<SoilMoistureSensor>> GetAllAsync();

    Task<SoilMoistureSensor?> GetByIdAsync(int id);

    Task<SoilMoistureSensor> CreateAsync(string description, int adcChannel, int dryValue, int wetValue);

    Task<SoilMoistureSensor?> UpdateConfigurationAsync(int id, string description, int dryValue, int wetValue,
        double minThresholdPercentage);

    Task<bool> DeleteAsync(int id);
}