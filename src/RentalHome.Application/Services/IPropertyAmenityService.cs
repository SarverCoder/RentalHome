using RentalHome.Application.Models.PropertyAmenity;

namespace RentalHome.Application.Services;

public interface IPropertyAmenityService
{
    Task<List<PropertyAmenityModel>> GetAllAsync();
    Task<PropertyAmenityModel?> GetAsync(int propertyId, int amenityId);
    Task<PropertyAmenityResponseModel> CreateAsync(PropertyAmenityModel dto);
    Task<PropertyAmenityResponseModel> DeleteAsync(int propertyId, int amenityId);

}
