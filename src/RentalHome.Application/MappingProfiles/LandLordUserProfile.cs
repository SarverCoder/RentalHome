using AutoMapper;
using RentalHome.Application.Models;
using RentalHome.Core.Entities;

namespace ProfChemistry.Application.MappingProfiles;

public class LandLordUserProfile : Profile
{
    public LandLordUserProfile()
    {

        CreateMap<CreateLandLordUserModel, User>().ReverseMap();
    }
}
