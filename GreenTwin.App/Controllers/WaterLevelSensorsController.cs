using GreenTwin.App.Dtos;
using GreenTwin.App.Interfaces;
using GreenTwin.App.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GreenTwin.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WaterLevelSensorsController : ControllerBase
{
    private readonly IWaterLevelSensorService _sensorService;

    public WaterLevelSensorsController(IWaterLevelSensorService sensorService)
    {
        _sensorService = sensorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WaterLevelSensor>>> GetSensors()
    {
        var sensors = await _sensorService.GetAllAsync();
        return Ok(sensors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WaterLevelSensor>> GetSensor(int id)
    {
        var sensor = await _sensorService.GetByIdAsync(id);
        return sensor is null ? NotFound() : Ok(sensor);
    }

    [HttpPost]
    public async Task<ActionResult<WaterLevelSensor>> CreateSensor(CreateWaterLevelSensorDto dto)
    {
        var newSensor = await _sensorService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetSensor), new { id = newSensor.Id }, newSensor);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WaterLevelSensor>> UpdateSensor(int id, UpdateWaterLevelSensorDto dto)
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
