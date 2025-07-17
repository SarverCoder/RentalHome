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
            #region
            var property = _mapper.Map<Property>(model);
            List<PropertyAmenity> results = model.PropertyAmenityIds.Select(a => new PropertyAmenity { AmenityId = a }).ToList();
            property.PropertyAmenities = results;
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            return new PropertyResponseModel
            {
                IsSuccess = true,
                Message = "Property created successfully",
                Id = property.Id
            };
            #endregion
            //var property = _mapper.Map<Property>(model);

            //// Property ni avval saqlab, Id olish kerak
            //await _context.Properties.AddAsync(property);
            //await _context.SaveChangesAsync(); // bu yerda property.Id hosil bo'ladi

            //// Amenity IDlar asosida PropertyAmenity yozuvlarini yaratish
            //if (model.PropertyAmenityIds != null && model.PropertyAmenityIds.Any())
            //{
            //    var existingAmenityIds = await _context.Amenities
            //        .Where(a => model.PropertyAmenityIds.Contains(a.Id))
            //        .Select(a => a.Id)
            //        .ToListAsync();

            //    var propertyAmenities = existingAmenityIds.Select(amenityId => new PropertyAmenity
            //    {
            //        PropertyId = property.Id,
            //        AmenityId = amenityId
            //    }).ToList();

            //    await _context.PropertyAmenities.AddRangeAsync(propertyAmenities);
            //    await _context.SaveChangesAsync();
            //}

            //return new PropertyResponseModel
            //{
            //    IsSuccess = true,
            //    Message = "Property created successfully",
            //    Id = property.Id
            //};
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
            var property = await _context.Properties
                .Include(p => p.PropertyAmenities)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (property == null)
            {
                return new PropertyResponseModel
                {
                    IsSuccess = false,
                    Message = "Property not found"
                };
            }

            // Update asosiy Property qiymatlari
            _mapper.Map(model, property);
            _context.Properties.Update(property);

            // Amenity ID larni yangilash
            if (model.PropertyAmenityIds != null)
            {
                // Eski yozuvlarni o'chiramiz
                _context.PropertyAmenities.RemoveRange(property.PropertyAmenities);

                // Faqat mavjud amenity id larni olish
                var validAmenityIds = await _context.Amenities
                    .Where(a => model.PropertyAmenityIds.Contains(a.Id))
                    .Select(a => a.Id)
                    .ToListAsync();

                // Yangilarini qo‘shamiz
                var newAmenities = validAmenityIds.Select(amenityId => new PropertyAmenity
                {
                    PropertyId = property.Id,
                    AmenityId = amenityId
                }).ToList();

                await _context.PropertyAmenities.AddRangeAsync(newAmenities);
            }

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
            var property = await _context.Properties
                .Include(p => p.PropertyAmenities)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (property == null)
            {
                return new PropertyResponseModel
                {
                    IsSuccess = false,
                    Message = "Property not found"
                };
            }

            // Bog‘langan PropertyAmenity yozuvlarini o‘chirish
            if (property.PropertyAmenities.Any())
            {
                _context.PropertyAmenities.RemoveRange(property.PropertyAmenities);
            }

            // Asosiy Propertyni o‘chirish
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
                .ThenInclude(l=>l.User)
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