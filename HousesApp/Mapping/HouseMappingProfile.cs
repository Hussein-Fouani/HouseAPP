using AutoMapper;
using HousesApp.Models;
using HousesApp.Models.Dto;

namespace HousesApp.Mapping;

public class HouseMappingProfile:Profile
{
    public HouseMappingProfile()
    {
        CreateMap<HouseModel, HouseDto>().ReverseMap();
        CreateMap<HouseModel, HouseCreateDto>().ReverseMap();
        CreateMap<HouseModel, UpdateHouseDto>().ReverseMap();
    }
}