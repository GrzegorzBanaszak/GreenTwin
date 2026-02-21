using GreenTwin.App.Domain;
using GreenTwin.App.Dtos;

namespace GreenTwin.App.Interfaces;

/// <summary>
/// Definiuje operacje do zarządzania czujnikami wilgotności gleby.
/// </summary>
public interface ISoilMoistureSensorService
{
    Task<IEnumerable<SoilMoistureSensor>> GetAllAsync();

    Task<SoilMoistureSensor?> GetByIdAsync(int id);

    Task<SoilMoistureSensor> CreateAsync(CreateSoilMoistureSensorDto dto);

    Task<SoilMoistureSensor?> UpdateConfigurationAsync(int id, UpdateSoilMoistureSensorDto dto);

    Task<bool> DeleteAsync(int id);
}