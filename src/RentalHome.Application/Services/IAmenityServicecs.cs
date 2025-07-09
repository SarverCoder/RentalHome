using RentalHome.Application.Models.Amenity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalHome.Application.Models;

namespace RentalHome.Application.Services
{
    public interface IAmenityService
    {
        Task<List<AmenityModel>> GetAllAsync();
        Task<AmenityModel> GetByIdAsync(int id);
        Task<ApiResult<string>> CreateAmenityAsync(CreateAmenityModel dto);
        Task<bool> UpdateAmenityAsync(UpdateAmenityModel dto, int id);
        Task<bool> DeleteAmenityAsync(int id);
    }

}
