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
        CreateMap<Property, PropertyModel>().ForMember(dest => dest.LandlordName, opt => opt.MapFrom(src => src.Landlord.User.UserName)).ReverseMap();
    }
}
