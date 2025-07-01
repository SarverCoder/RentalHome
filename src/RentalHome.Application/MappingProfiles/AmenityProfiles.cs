using AutoMapper;
using RentalHome.Application.Models;
using RentalHome.Application.Models.Amenity;
using RentalHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHome.Application.MappingProfiles
{
    public  class AmenityProfiles: Profile
    {
        public AmenityProfiles()
        {
            CreateMap<CreateAmenityModel, Amenity>().ReverseMap();
            CreateMap<UpdateAmenityModel, Amenity>().ReverseMap();
            CreateMap<Amenity, AmenityModel>().ReverseMap();
        }
    }
}
