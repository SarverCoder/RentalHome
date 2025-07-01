using AutoMapper;
using RentalHome.Application.Models.Landlord;
using RentalHome.Core.Entities;

namespace RentalHome.Application.MappingProfiles;

public class LandlordProfile : Profile
{
    public LandlordProfile()
    {
        CreateMap<CreateLandlordModel, Landlord>();
        CreateMap<UpdateLandlordModel, Landlord>();
        CreateMap<Landlord, LandlordModel>().ReverseMap();
    }
}