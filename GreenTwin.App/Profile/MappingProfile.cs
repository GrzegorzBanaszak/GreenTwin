using AutoMapper;
using GreenTwin.App.Application.Dtos;
using GreenTwin.App.Domain;

namespace GreenTwin.App.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateSoilMoistureSensorDto, SoilMoistureSensor>();
        CreateMap<UpdateSoilMoistureSensorDto, SoilMoistureSensor>();
    }
}