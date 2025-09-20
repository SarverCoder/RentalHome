using AutoMapper;
using RentalHome.Application.Models.PropertyModel;
using RentalHome.Core.Entities;

namespace RentalHome.Application.MappingProfiles;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<Photo, ImageGetDto>();
    }
}