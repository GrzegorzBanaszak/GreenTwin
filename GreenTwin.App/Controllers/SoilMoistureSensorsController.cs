using GreenTwin.App.Application.Dtos;
using GreenTwin.App.Application.Interfaces;
using GreenTwin.App.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GreenTwin.App.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SoilMoistureSensorsController : ControllerBase
{
    private readonly ISoilMoistureSensorService _sensorService;

    public SoilMoistureSensorsController(ISoilMoistureSensorService sensorService)
    {
        _sensorService = sensorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SoilMoistureSensor>>> GetSensors()
    {
        var sensors = await _sensorService.GetAllAsync();
        return Ok(sensors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SoilMoistureSensor>> GetSensor(int id)
    {
        var sensor = await _sensorService.GetByIdAsync(id);
        return sensor is null ? NotFound() : Ok(sensor);
    }

    [HttpPost]
    public async Task<ActionResult<SoilMoistureSensor>> CreateSensor(CreateSoilMoistureSensorDto dto)
    {
        var newSensor = await _sensorService.CreateAsync(dto.Description, dto.AdcChannel, dto.DryValue, dto.WetValue);
        return CreatedAtAction(nameof(GetSensor), new { id = newSensor.Id }, newSensor);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SoilMoistureSensor>> UpdateSensor(int id, UpdateSoilMoistureSensorDto dto)
    {
        var updatedSensor = await _sensorService.UpdateConfigurationAsync(id, dto);

        return updatedSensor is null ? NotFound() : Ok(updatedSensor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSensor(int id)
    {
        var success = await _sensorService.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}