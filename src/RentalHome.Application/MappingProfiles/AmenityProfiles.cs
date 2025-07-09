using AutoMapper;
using RentalHome.Application.Models.Amenity;
using RentalHome.Core.Entities;

namespace RentalHome.Application.MappingProfiles;

public  class AmenityProfiles: Profile
{
    public AmenityProfiles()
    {
        CreateMap<CreateAmenityModel, Amenity>().ReverseMap();
        CreateMap<UpdateAmenityModel, Amenity>().ReverseMap();
        CreateMap<Amenity, AmenityModel>().ReverseMap();
    }
}
