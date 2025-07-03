using AutoMapper;
using RentalHome.Application.Models.District;
using RentalHome.Core.Entities;

namespace RentalHome.Application.MappingProfiles;

public class DistrictProfile : Profile
{
    public DistrictProfile()
    {
        CreateMap<CreateDistrictModel, District>(); 
        CreateMap<UpdateDistrictModel, District>();
        CreateMap<DistrictModel, District>().ReverseMap();
    }
}