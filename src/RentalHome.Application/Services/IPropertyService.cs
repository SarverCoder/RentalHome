using RentalHome.Application.Models.PropertyModel;
using RentalHome.Core.Entities;

namespace RentalHome.Application.Services;

public interface IPropertyService
{
    Task<List<PropertyModel>> GetAllAsync();
    Task<PropertyModel?> GetByIdAsync(int id);
    Task<PropertyResponseModel> CreateAsync(CreatePropertyModel model);
    Task<PropertyResponseModel> UpdateAsync( UpdatePropertyModel model,int id);
    Task<PropertyResponseModel> DeleteAsync(int id);
}
