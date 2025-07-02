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

    public async Task<Property> CreateAsync(CreatePropertyModel model)
    {
        var property = _mapper.Map<Property>(model);
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();
        return property;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property is null) return false;

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Property>> GetAllAsync()
    {
        return await _context.Properties.ToListAsync();
    }

    public async Task<Property?> GetByIdAsync(int id)
    {
        return await _context.Properties.FindAsync(id);
    }

    public async Task<bool> UpdateAsync(int id, UpdatePropertyModel model)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null) return false;

        _mapper.Map(model, property);
        await _context.SaveChangesAsync();
        return true;
    }
}