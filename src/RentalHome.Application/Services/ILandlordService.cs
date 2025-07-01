using RentalHome.Application.Models;
using RentalHome.Application.Models.Landlord;
using RentalHome.Core.Entities;

namespace RentalHome.Application.Services;

public interface ILandlordService
{
    Task AddAsync(CreateLandlordModel landlord);
    Task<bool> UpdateAsync(UpdateLandlordModel landlord);
    Task<bool> DeleteAsync(int id);
    Task<LandlordModel> GetByIdAsync(int id);
    Task<List<LandlordModel>> GetAllAsync();    
    
}