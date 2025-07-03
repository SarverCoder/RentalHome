using AutoMapper;
using RentalHome.Application.Models.Region;
using RentalHome.Core.Entities;

namespace RentalHome.Application.MappingProfiles;

public class RegionProfile : Profile
{
    public RegionProfile()
    {
        CreateMap<CreateRegionModel, Region>();
        CreateMap<UpdateRegionModel, Region>();
        CreateMap<RegionModel,Region>().ReverseMap();   
    }
}