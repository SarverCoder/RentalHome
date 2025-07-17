using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Models;
using RentalHome.Application.Models.Amenity;
using RentalHome.Core.Entities;
using RentalHome.Core.Exceptions;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services.Implementation;

public class AmenityService (DatabaseContext context ,IMapper  mapper) : IAmenityService
{
  
    public async Task<ApiResult<string>> CreateAmenityAsync(CreateAmenityModel dto)
    {
        var amenity = mapper.Map<Amenity>(dto);
        await context.Amenities.AddAsync(amenity);
        await context.SaveChangesAsync();
        return ApiResult<string>.Success("Amenity successfully created");
    }

    public async Task<bool> DeleteAmenityAsync(int id)
    {
        var amenity = await context.Amenities.FirstOrDefaultAsync(k => k.Id == id);
        if (amenity is null)
        {
            throw new NotFoundException("Id not found");
        }
        context.Amenities.Remove(amenity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<AmenityModel>> GetAllAsync()
    {
        var amenities = await context.Amenities.ToListAsync();
        var amenityModels = mapper.Map<List<AmenityModel>>(amenities);
        return amenityModels;
    }

    public async Task<AmenityModel> GetByIdAsync(int id)
    {
        var amenity = await context.Amenities.FirstOrDefaultAsync(x => x.Id == id);
        if (amenity is null)
        {
            throw new NotFoundException("Amenity not found");
        }

        return mapper.Map<AmenityModel>(amenity);
    }


    public async Task<bool> UpdateAmenityAsync(UpdateAmenityModel dto, int id)
    {
        var amenity = await context.Amenities.FirstOrDefaultAsync(x => x.Id == id);

        if (amenity is null)
        {
            throw new NotFoundException("Amenity not found");
            return false;
        }
        amenity.Name = dto.Name;
        amenity.IconClass = dto.IconClass;

        context.Update(amenity);
        
        await context.SaveChangesAsync();
        return true;

    }
}
