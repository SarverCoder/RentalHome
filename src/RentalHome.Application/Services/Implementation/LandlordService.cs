using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentalHome.Application.MappingProfiles;
using RentalHome.Application.Models;
using RentalHome.Application.Models.Landlord;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services;

public class LandlordService(DatabaseContext context,IMapper mapper) : ILandlordService
{

    public async Task AddAsync(CreateLandlordModel createLandlord)
    {
        var landlord = mapper.Map<CreateLandlordModel, Landlord>(createLandlord);
        await context.Landlords.AddAsync(landlord);
        await context.SaveChangesAsync();
    }

    public Task<bool> UpdateAsync(UpdateLandlordModel landlord)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var landlord = await context.Landlords.FindAsync(id);
        
        if (landlord is null) return false;  
        context.Remove(landlord);
        
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<LandlordModel> GetByIdAsync(int id)
    {
        var landlord = await context.Landlords.FindAsync(id);
        return  mapper.Map<Landlord, LandlordModel>(landlord);
    }

    public async Task<List<LandlordModel>> GetAllAsync()
    {
        var  landlords = await context.Landlords.ToListAsync();
        var landlorModels = mapper.Map<List<Landlord>, List<LandlordModel>>(landlords);
        return await Task.FromResult(landlorModels);
    }
}