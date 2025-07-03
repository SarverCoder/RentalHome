using RentalHome.Application.Models.Region;

namespace RentalHome.Application.Services;

public interface IRegionService
{
    Task<List<RegionModel>> GetAllAsync();
    Task<RegionModel> GetByIdAsync(int id);
    Task<RegionResponseModel> AddAsync(CreateRegionModel model);
    Task<RegionResponseModel> UpdateAsync(UpdateRegionModel model, int id);
    Task<RegionResponseModel> DeleteAsync(int id);
}