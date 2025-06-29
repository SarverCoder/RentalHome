using AutoMapper;
using RentalHome.Application.Models;
using RentalHome.Core.Entities;

namespace RentalHome.Application.MappingProfiles;

public class LandLordUserProfile : Profile
{
    public LandLordUserProfile()
    {

        CreateMap<CreateLandLordUserModel, User>().ReverseMap();
    }
}
