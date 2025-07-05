using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Models.PropertyModel;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services.Implementation;

public class PropertyService : IPropertyService
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public PropertyService(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PropertyResponseModel> CreateAsync(CreatePropertyModel model)
    {
        try
        {
            if (await _context.Properties.AnyAsync(p => p.Title == model.Title))
            {
                return new PropertyResponseModel
                {
                    IsSuccess = false,
                    Message = "Property with this title already exists"
                };
            }

            var property = _mapper.Map<Property>(model);
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            return new PropertyResponseModel
            {
                IsSuccess = true,
                Message = "Property created successfully",
                Id = property.Id
            };
        }
        catch (Exception ex)
        {
            return new PropertyResponseModel
            {
                IsSuccess = false,
                Message = $"Failed to create property: {ex.Message}"
            };
        }
    }

    public async Task<PropertyResponseModel> UpdateAsync(UpdatePropertyModel model, int id)
    {
        try
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return new PropertyResponseModel
                {
                    IsSuccess = false,
                    Message = "Property not found"
                };
            }

            if (await _context.Properties.AnyAsync(p => p.Title == model.Title && p.Id != id))
            {
                return new PropertyResponseModel
                {
                    IsSuccess = false,
                    Message = "Another property with this title already exists"
                };
            }

            _mapper.Map(model, property);
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();

            return new PropertyResponseModel
            {
                IsSuccess = true,
                Message = "Property updated successfully",
                Id = property.Id
            };
        }
        catch (Exception ex)
        {
            return new PropertyResponseModel
            {
                IsSuccess = false,
                Message = $"Failed to update property: {ex.Message}"
            };
        }
    }

    public async Task<PropertyResponseModel> DeleteAsync(int id)
    {
        try
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return new PropertyResponseModel
                {
                    IsSuccess = false,
                    Message = "Property not found"
                };
            }

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            return new PropertyResponseModel
            {
                IsSuccess = true,
                Message = "Property deleted successfully"
            };
        }
        catch (Exception ex)
        {
            return new PropertyResponseModel
            {
                IsSuccess = false,
                Message = $"Failed to delete property: {ex.Message}"
            };
        }
    }

    public async Task<PropertyModel?> GetByIdAsync(int id)
    {
        var property = await _context.Properties
            .Include(p => p.Region)
            .Include(p => p.District)
            .Include(p => p.Landlord)
            .Include(p => p.Photos)
            .Include(p => p.PropertyAmenities).ThenInclude(pa => pa.Amenity)
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.Id == id);

        return _mapper.Map<PropertyModel>(property);
    }

    public async Task<List<PropertyModel>> GetAllAsync()
    {
        var properties = await _context.Properties
            .Include(p => p.Region)
            .Include(p => p.District)
            .Include(p => p.Photos)
            .ToListAsync();

        return _mapper.Map<List<PropertyModel>>(properties);
    }

}