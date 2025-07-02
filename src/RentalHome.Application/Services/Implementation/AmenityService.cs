using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Models.Amenity;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services.Implementation;

public class AmenityService (DatabaseContext context ,IMapper  mapper) : IAmenityService
{
  
    public async Task CreateAmenityAsync(CreateAmenityModel dto)
    {
        var amenity = mapper.Map<Amenity>(dto);
        await context.Amenities.AddAsync(amenity);
        await context.SaveChangesAsync();

    }

    public async Task<bool> DeleteAmenityAsync(int id)
    {
        var amenity = await context.Amenities.FirstOrDefaultAsync(k => k.Id == id);
        if (amenity == null)
        {
            return false;
        }
        context.Amenities.Remove(amenity);
        await context.SaveChangesAsync();
        return true;
    }

    public Task<List<AmenityModel>> GetAllAsync()
    {
        var amenities = context.Amenities.ToList();
        var amenityModels = mapper.Map<List<AmenityModel>>(amenities);
        return Task.FromResult(amenityModels);
    }

    public Task<AmenityModel> GetByIdAsync(int id)
    {
        var amenity = context.Amenities.Find(id);
        if (amenity == null)
        {
            return Task.FromResult<AmenityModel>(null);
        }
        var amenityModel = mapper.Map<AmenityModel>(amenity);
        return Task.FromResult(amenityModel);                                       
    }


    public Task<bool> UpdateeAmenityAsync(UpdateAmenityModel dto)
    {
        var amenity = context.Amenities.Find(dto.Id);
        if (amenity == null)
        {
            return Task.FromResult(false);
        }
        amenity.Name = dto.Name;
        amenity.IconClass = dto.Icon;
        context.Amenities.Update(amenity);
        return Task.FromResult(true);
    }
}
