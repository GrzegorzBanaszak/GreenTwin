using System.Collections.Concurrent;
using AutoMapper;
using GreenTwin.App.Domain;
using GreenTwin.App.Dtos;
using GreenTwin.App.Interfaces;

namespace GreenTwin.App.Services;

/// <summary>
/// Serwis do zarządzania czujnikami poziomu wody.
/// W tej implementacji używa magazynu w pamięci.
/// </summary>
public class WaterLevelSensorService : IWaterLevelSensorService
{
    private readonly ConcurrentDictionary<int, WaterLevelSensor> _sensors = new();
    private readonly IMapper _mapper;
    private int _nextId = 0;

    public WaterLevelSensorService(IMapper mapper)
    {
        _mapper = mapper;

        // Wstępne dane do symulacji
        CreateAsync(new CreateWaterLevelSensorDto
        {
            Description = "Główny zbiornik na deszczówkę",
            EmptyDistanceCm = 200,
            FullDistanceCm = 10,
            CapacityLiters = 1000
        });
    }

    public Task<IEnumerable<WaterLevelSensor>> GetAllAsync()
    {
        var sortedSensors = _sensors.Values.OrderBy(s => s.Id);
        return Task.FromResult<IEnumerable<WaterLevelSensor>>(sortedSensors);
    }

    public Task<WaterLevelSensor?> GetByIdAsync(int id)
    {
        _sensors.TryGetValue(id, out var sensor);
        return Task.FromResult(sensor);
    }

    public Task<WaterLevelSensor> CreateAsync(CreateWaterLevelSensorDto dto)
    {
        var id = Interlocked.Increment(ref _nextId);
        var sensor = new WaterLevelSensor(id, dto.Description, dto.EmptyDistanceCm, dto.FullDistanceCm, dto.CapacityLiters);

        if (!_sensors.TryAdd(id, sensor))
        {
            throw new InvalidOperationException("Nie udało się dodać czujnika z powodu konfliktu ID.");
        }

        return Task.FromResult(sensor);
    }

    public async Task<WaterLevelSensor?> UpdateConfigurationAsync(int id, UpdateWaterLevelSensorDto dto)
    {
        var sensor = await GetByIdAsync(id);
        if (sensor is null)
        {
            return null;
        }

        _mapper.Map(dto, sensor);

        return sensor;
    }

    public Task<bool> DeleteAsync(int id)
    {
        return Task.FromResult(_sensors.TryRemove(id, out _));
    }
}
