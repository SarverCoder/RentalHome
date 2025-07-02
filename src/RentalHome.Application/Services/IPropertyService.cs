using RentalHome.Application.Models.PropertyModel;
using RentalHome.Core.Entities;

namespace RentalHome.Application.Services;

public interface IPropertyService
{
    Task<List<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync(int id);
    Task<Property> CreateAsync(CreatePropertyModel model);
    Task<bool> UpdateAsync(int id, UpdatePropertyModel model);
    Task<bool> DeleteAsync(int id);
}
