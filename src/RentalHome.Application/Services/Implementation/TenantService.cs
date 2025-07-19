using AutoMapper;
using RentalHome.Application.Models.Tenant;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services.Implementation;

public class TenantService : ITenantService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IMapper _mapper;


    public TenantService(DatabaseContext context, IMapper    mapper, DatabaseContext databaseContext)
    {
        _mapper = mapper;
        _databaseContext = databaseContext;
    }

    public async Task CreateAsync(CreateTenantModel createTenantModel)
    {
        var tenant = _mapper.Map<Tenant>(createTenantModel);
        await _databaseContext.AddAsync(tenant);
        await _databaseContext.SaveChangesAsync();

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
