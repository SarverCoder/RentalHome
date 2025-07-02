using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Models.PropertyModel;
using RentalHome.Application.Services.Interfaces;
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

    public async Task<Property> CreateAsync(CreatePropertyModel model)
    {
        var property = _mapper.Map<Property>(model);
        _context.Property.Add(property);
        await _context.SaveChangesAsync();
        return property;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var property = await _context.Property.FindAsync(id);
        if (property is null) return false;

        _context.Property.Remove(property);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Property>> GetAllAsync()
    {
        return await _context.Property.ToListAsync();
    }

    public async Task<Property?> GetByIdAsync(int id)
    {
        return await _context.Property.FindAsync(id);
    }

    public async Task<bool> UpdateAsync(int id, UpdatePropertyModel model)
    {
        var property = await _context.Property.FindAsync(id);
        if (property == null) return false;

        _mapper.Map(model, property);
        await _context.SaveChangesAsync();
        return true;
    }
}