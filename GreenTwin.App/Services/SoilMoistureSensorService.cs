using System.Collections.Concurrent;
using AutoMapper;
using GreenTwin.App.Dtos;
using GreenTwin.App.Interfaces;
using GreenTwin.App.Domain;

namespace GreenTwin.App.Services;

/// <summary>
/// Serwis do zarządzania czujnikami wilgotności gleby.
/// W tej implementacji używa magazynu w pamięci.
/// </summary>
public class SoilMoistureSensorService : ISoilMoistureSensorService
{
    private readonly List<SoilMoistureSensor> _sensors = new();
    private readonly IMapper _mapper;
    private int _nextId = 0;

    public SoilMoistureSensorService(IMapper mapper)
    {
        _mapper = mapper;
        _sensors.Add(new SoilMoistureSensor(1, "Pomidory", 0, 2.407, 1.12));
        _sensors.Add(new SoilMoistureSensor(2, "Ogórki", 1, 2.407, 1.125));
    }

    public Task<IEnumerable<SoilMoistureSensorDto>> GetAllAsync()
    {
        _sensors.ForEach(s => s.UpdateMoisture());

        return Task.FromResult(_mapper.Map<IEnumerable<SoilMoistureSensorDto>>(_sensors));
    }

    public Task<SoilMoistureSensorDto?> GetByIdAsync(int id)
    {
        var sensor = _sensors.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(_mapper.Map<SoilMoistureSensorDto?>(sensor));
    }

    public Task<SoilMoistureSensorDto> CreateAsync(CreateSoilMoistureSensorDto dto)
    {


        if (_sensors.Any(s => s.AdcChannel == dto.AdcChannel))
        {
            // W praktyce nie powinno się zdarzyć przy użyciu Interlocked
            throw new InvalidOperationException("Pod ten kanał ADC już jest przypisany czujnik.");
        }
        var id = Interlocked.Increment(ref _nextId);
        var sensor = new SoilMoistureSensor(id, dto.Description, dto.AdcChannel, dto.DryValue, dto.WetValue);
        return Task.FromResult(_mapper.Map<SoilMoistureSensorDto>(sensor));
    }

    public async Task<SoilMoistureSensorDto?> UpdateConfigurationAsync(int id, UpdateSoilMoistureSensorDto dto)
    {
        var sensor = _sensors.FirstOrDefault(s => s.Id == id);
        if (sensor is null)
        {
            return null;
        }

        // Używamy AutoMappera do nałożenia zmian z DTO na istniejącą encję
        _mapper.Map(dto, sensor);


        return _mapper.Map<SoilMoistureSensorDto>(sensor);
    }

    public Task<bool> DeleteAsync(int id)
    {
        int index = _sensors.RemoveAll(s => s.Id == id);
        return Task.FromResult(index > 0);

    }
}