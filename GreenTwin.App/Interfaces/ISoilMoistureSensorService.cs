using GreenTwin.App.Domain;
using GreenTwin.App.Dtos;

namespace GreenTwin.App.Interfaces;

/// <summary>
/// Definiuje operacje do zarządzania czujnikami wilgotności gleby.
/// </summary>
public interface ISoilMoistureSensorService
{
    Task<IEnumerable<SoilMoistureSensorDto>> GetAllAsync();

    Task<SoilMoistureSensorDto?> GetByIdAsync(int id);

    Task<SoilMoistureSensorDto> CreateAsync(CreateSoilMoistureSensorDto dto);

    Task<SoilMoistureSensorDto?> UpdateConfigurationAsync(int id, UpdateSoilMoistureSensorDto dto);

    Task<bool> DeleteAsync(int id);
}