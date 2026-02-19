using GreenTwin.App.Domain;
using GreenTwin.App.Application.Dtos;

namespace GreenTwin.App.Application.Interfaces;

/// <summary>
/// Definiuje operacje do zarządzania czujnikami wilgotności gleby.
/// </summary>
public interface ISoilMoistureSensorService
{
    Task<IEnumerable<SoilMoistureSensor>> GetAllAsync();

    Task<SoilMoistureSensor?> GetByIdAsync(int id);

    Task<SoilMoistureSensor> CreateAsync(string description, int adcChannel, int dryValue, int wetValue);

    Task<SoilMoistureSensor?> UpdateConfigurationAsync(int id, UpdateSoilMoistureSensorDto dto);

    Task<bool> DeleteAsync(int id);
}