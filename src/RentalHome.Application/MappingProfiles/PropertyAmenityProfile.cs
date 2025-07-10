using AutoMapper;
using RentalHome.Application.Models.PropertyAmenity;
using RentalHome.Core.Entities;

namespace RentalHome.Application.MappingProfiles;

public class PropertyAmenityProfile : Profile
{
    public PropertyAmenityProfile()
    {
        CreateMap<PropertyAmenity, PropertyAmenityModel>().ReverseMap();
            
    }
}
