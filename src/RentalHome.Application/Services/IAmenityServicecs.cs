using RentalHome.Application.Models.Amenity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHome.Application.Services
{
    public interface IAmenityService
    {
        Task<List<AmenityModel>> GetAllAsync();
        Task<AmenityModel> GetByIdAsync(int id);
        Task CreateAmenityAsync(CreateAmenityModel dto);
        Task<bool> UpdateeAmenityAsync(UpdateAmenityModel dto);
        Task<bool> DeleteAmenityAsync(int id);
    }

}
