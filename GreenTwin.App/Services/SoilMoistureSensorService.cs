using System.Collections.Concurrent;
using AutoMapper;
using GreenTwin.App.Application.Dtos;
using GreenTwin.App.Application.Interfaces;
using GreenTwin.App.Domain;

namespace GreenTwin.App.Application.Services;

/// <summary>
/// Serwis do zarządzania czujnikami wilgotności gleby.
/// W tej implementacji używa magazynu w pamięci.
/// </summary>
public class SoilMoistureSensorService : ISoilMoistureSensorService
{
    private readonly ConcurrentDictionary<int, SoilMoistureSensor> _sensors = new();
    private readonly IMapper _mapper;
    private int _nextId = 0;

    public SoilMoistureSensorService(IMapper mapper)
    {
        _mapper = mapper;


        // Wstępne dane do symulacji
        CreateAsync("Donica - Papryka Chili", 0, 20000, 10000);
        CreateAsync("Sekcja Północna", 1, 21500, 11000);
    }

    public Task<IEnumerable<SoilMoistureSensor>> GetAllAsync()
    {
        var sortedSensors = _sensors.Values.OrderBy(s => s.Id);
        return Task.FromResult<IEnumerable<SoilMoistureSensor>>(sortedSensors);
    }

    public Task<SoilMoistureSensor?> GetByIdAsync(int id)
    {
        _sensors.TryGetValue(id, out var sensor);
        return Task.FromResult(sensor);
    }

    public Task<SoilMoistureSensor> CreateAsync(string description, int adcChannel, int dryValue, int wetValue)
    {
        var id = Interlocked.Increment(ref _nextId);
        var sensor = new SoilMoistureSensor(id, description, adcChannel, dryValue, wetValue);

        if (!_sensors.TryAdd(id, sensor))
        {
            // W praktyce nie powinno się zdarzyć przy użyciu Interlocked
            throw new InvalidOperationException("Nie udało się dodać czujnika z powodu konfliktu ID.");
        }

        return Task.FromResult(sensor);
    }

    public async Task<SoilMoistureSensor?> UpdateConfigurationAsync(int id, UpdateSoilMoistureSensorDto dto)
    {
        var sensor = await GetByIdAsync(id);
        if (sensor is null)
        {
            return null;
        }

        // Używamy AutoMappera do nałożenia zmian z DTO na istniejącą encję
        _mapper.Map(dto, sensor);

        return sensor;
    }

    public Task<bool> DeleteAsync(int id)
    {
        return Task.FromResult(_sensors.TryRemove(id, out _));
    }
}