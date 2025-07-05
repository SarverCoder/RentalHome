using AutoMapper;
using RentalHome.Application.Models.PropertyModel;
using RentalHome.Core.Entities;

namespace RentalHome.Application.MappingProfiles;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<CreatePropertyModel, Property>();
        CreateMap<UpdatePropertyModel, Property>();
        CreateMap<Property, PropertyModel>().ReverseMap();
    }
}
