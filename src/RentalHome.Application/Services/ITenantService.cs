using RentalHome.Application.Models.Tenant;
using RentalHome.Core.Entities;

namespace RentalHome.Application.Services;

public interface ITenantService
{
    Task<List<Tenant>> GetAllAsync();
    Task<Tenant?> GetByIdAsync(int id);
    Task CreateAsync(CreateTenantModel createTenantModel);
    Task<bool> UpdateAsync(int id, CreateTenantModel createTenantModel);
    Task<bool> DeleteAsync(int id);
}
