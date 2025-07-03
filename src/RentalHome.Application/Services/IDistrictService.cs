using RentalHome.Application.Models.District;

namespace RentalHome.Application.Services;

public interface IDistrictService
{
    Task<DistrictResponseModel> AddAsync(CreateDistrictModel landlord);
    Task<DistrictResponseModel> UpdateAsync(UpdateDistrictModel landlord, int id);
    Task<DistrictResponseModel> DeleteAsync(int id);
    Task<DistrictModel> GetByIdAsync(int id);
    Task<List<DistrictModel>> GetAllAsync();       
}