using AutoMapper;
using GreenTwin.App.Dtos;
using GreenTwin.App.Domain;

namespace GreenTwin.App.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateSoilMoistureSensorDto, SoilMoistureSensor>();
        CreateMap<UpdateSoilMoistureSensorDto, SoilMoistureSensor>();

        CreateMap<CreateWaterLevelSensorDto, WaterLevelSensor>();
        CreateMap<UpdateWaterLevelSensorDto, WaterLevelSensor>();
    }
}