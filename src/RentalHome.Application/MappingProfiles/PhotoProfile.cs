using AutoMapper;
using RentalHome.Application.Models.Photo;
using RentalHome.Core.Entities;

namespace RentalHome.Application.MappingProfiles;

public class PhotoProfile : Profile
{
    public PhotoProfile()
    {
        CreateMap<CreatePhotoModel, Photo>().ReverseMap();

        CreateMap<UpdatePhotoModel, Photo>().ReverseMap();

        CreateMap<Photo, PhotoModel>().ReverseMap();

        CreateMap<CreatePhotoModel, ResponsePhotoModel>().ReverseMap();
    }
}