using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Models.PropertyAmenity;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services.Implementation;

public class PropertyAmenityService : IPropertyAmenityService
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public PropertyAmenityService(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PropertyAmenityModel>> GetAllAsync()
    {
        var propertyAmenities = await _context.PropertyAmenities
            .Include(pa => pa.Amenity)
            .ToListAsync();

        return _mapper.Map<List<PropertyAmenityModel>>(propertyAmenities);
    }

    public async Task<PropertyAmenityModel?> GetAsync(int propertyId, int amenityId)
    {
        var entity = await _context.PropertyAmenities
            .Include(pa => pa.Amenity)
            .FirstOrDefaultAsync(pa => pa.PropertyId == propertyId && pa.AmenityId == amenityId);

        return entity is not null ? _mapper.Map<PropertyAmenityModel>(entity) : null;
    }

    public async Task<PropertyAmenityResponseModel> CreateAsync(PropertyAmenityModel dto)
    {
        try
        {
            // ForeignKey mavjudligini tekshirish
            if (!await _context.Properties.AnyAsync(p => p.Id == dto.PropertyId))
                return new PropertyAmenityResponseModel { IsSuccess = false, Message = "Property not found." };

            if (!await _context.Amenities.AnyAsync(a => a.Id == dto.AmenityId))
                return new PropertyAmenityResponseModel { IsSuccess = false, Message = "Amenity not found." };

            // Duplicate tekshirish
            var exists = await _context.PropertyAmenities
                .AnyAsync(pa => pa.PropertyId == dto.PropertyId && pa.AmenityId == dto.AmenityId);

            if (exists)
                return new PropertyAmenityResponseModel { IsSuccess = false, Message = "Already assigned." };

            // Create entity (manual mapping)
            var entity = new PropertyAmenity
            {
                PropertyId = dto.PropertyId,
                AmenityId = dto.AmenityId
            };

            await _context.PropertyAmenities.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new PropertyAmenityResponseModel
            {
                IsSuccess = true,
                Message = "Amenity assigned successfully."
            };
        }
        catch (Exception ex)
        {
            return new PropertyAmenityResponseModel
            {
                IsSuccess = false,
                Message = $"Error: {ex.InnerException?.Message ?? ex.Message}"
            };
        }
    }

    public async Task<PropertyAmenityResponseModel> DeleteAsync(int propertyId, int amenityId)
    {
        try
        {
            var entity = await _context.PropertyAmenities
                .FirstOrDefaultAsync(pa => pa.PropertyId == propertyId && pa.AmenityId == amenityId);

            if (entity == null)
            {
                return new PropertyAmenityResponseModel
                {
                    IsSuccess = false,
                    Message = "Amenity not found for this property."
                };
            }

            _context.PropertyAmenities.Remove(entity);
            await _context.SaveChangesAsync();

            return new PropertyAmenityResponseModel
            {
                IsSuccess = true,
                Message = "Amenity removed from property successfully."
            };
        }
        catch (Exception ex)
        {
            return new PropertyAmenityResponseModel
            {
                IsSuccess = false,
                Message = $"Failed to delete amenity: {ex.Message}"
            };
        }
    }
}
