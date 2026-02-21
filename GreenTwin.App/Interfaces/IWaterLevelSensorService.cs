using GreenTwin.App.Domain;
using GreenTwin.App.Dtos;

namespace GreenTwin.App.Interfaces;

/// <summary>
/// Definiuje operacje do zarządzania czujnikami poziomu wody.
/// </summary>
public interface IWaterLevelSensorService
{
    Task<IEnumerable<WaterLevelSensor>> GetAllAsync();

    Task<WaterLevelSensor?> GetByIdAsync(int id);

    Task<WaterLevelSensor> CreateAsync(CreateWaterLevelSensorDto dto);

    Task<WaterLevelSensor?> UpdateConfigurationAsync(int id, UpdateWaterLevelSensorDto dto);

    Task<bool> DeleteAsync(int id);
}
