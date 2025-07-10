using AutoMapper;
using RentalHome.Application.Models.PropertyModel;
using RentalHome.Core.Entities;

namespace RentalHome.Application.MappingProfiles;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<CreatePropertyModel, Property>().ReverseMap();
        CreateMap<UpdatePropertyModel, Property>().ReverseMap();
        CreateMap<Property, PropertyModel>()
    .ForMember(dest => dest.RegionName,
        opt => opt.MapFrom(src => src.Region.Name))
    .ForMember(dest => dest.DistrictName,
        opt => opt.MapFrom(src => src.District.Name))
    .ForMember(dest => dest.LandlordName,
        opt => opt.MapFrom(src => src.Landlord.User.UserName))
    .ForMember(dest => dest.PhotoUrls,
        opt => opt.MapFrom(src => src.Photos.Select(p => p.Url)))
    .ForMember(dest => dest.Amenities,
        opt => opt.MapFrom(src => src.PropertyAmenities.Select(pa => pa.Amenity)));
    }
}
