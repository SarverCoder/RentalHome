using AutoMapper;
using RentalHome.Application.Models.Tenant;
using RentalHome.Application.Services.Intefaces;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services.Implementation;

public class TenantService : ITenantService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IMapper _mapper;


    public TenantService(DatabaseContext context, IMapper    mapper)
    {
        
    }

    public Task<Tenant> CreateAsync(CreateTenantModel createTenantModel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Tenant>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Tenant?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, CreateTenantModel createTenantModel)
    {
        throw new NotImplementedException();
    }
}
