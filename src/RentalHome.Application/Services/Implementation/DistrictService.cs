using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Models.District;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services.Implementation;

public class DistrictService : IDistrictService
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public DistrictService(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DistrictResponseModel> AddAsync(CreateDistrictModel createDistrictModel)
    {
        try
        {
            if (await _context.Districts.AnyAsync(d => d.Name == createDistrictModel.Name))
            {
                return new DistrictResponseModel
                {
                    IsSuccess = false,
                    Message = "District with this name already exists"
                };
            }

            var district = _mapper.Map<District>(createDistrictModel);
            
            await _context.Districts.AddAsync(district);
            await _context.SaveChangesAsync();

            return new DistrictResponseModel
            {
                IsSuccess = true,
                Message = "District created successfully"
            };
        }
        catch (Exception ex)
        {
            return new DistrictResponseModel
            {
                IsSuccess = false,
                Message = $"Failed to create district: {ex.Message}"
            };
        }
    }

    public  async Task<DistrictResponseModel> UpdateAsync(UpdateDistrictModel updateDistrictModel, int id)
    {
        try
        {
            var district = await _context.Districts.FindAsync(id);
            if (district == null)
            {
                return new DistrictResponseModel
                {
                    IsSuccess = false,
                    Message = "District not found"
                };
            }

            if (await _context.Districts.AnyAsync(d => d.Name == updateDistrictModel.Name && d.Id != id))
            {
                return new DistrictResponseModel
                {
                    IsSuccess = false,
                    Message = "Another district with this name already exists"
                };
            }

            _mapper.Map(updateDistrictModel, district);
            _context.Districts.Update(district);
            await _context.SaveChangesAsync();

            return new DistrictResponseModel
            {
                IsSuccess = true,
                Message = "District updated successfully",
            };
        }
        catch (Exception ex)
        {
            return new DistrictResponseModel
            {
                IsSuccess = false,
                Message = $"Failed to update district: {ex.Message}"
            };
        }
    }

    public async Task<DistrictResponseModel> DeleteAsync(int id)
    {
        try
        {
            var district = await _context.Districts.FindAsync(id);
            if (district == null)
            {
                return new DistrictResponseModel
                {
                    IsSuccess = false,
                    Message = "District not found"
                };
            }

            _context.Districts.Remove(district);
            await _context.SaveChangesAsync();

            return new DistrictResponseModel
            {
                IsSuccess = true,
                Message = "District deleted successfully"
            };
        }
        catch (Exception ex)
        {
            return new DistrictResponseModel
            {
                IsSuccess = false,
                Message = $"Failed to delete district: {ex.Message}"
            };
        }
    }

    public async Task<DistrictModel> GetByIdAsync(int id)
    {
        var district = await _context.Districts
            .Include(d => d.Region)
            .FirstOrDefaultAsync(d => d.Id == id);
            
        return _mapper.Map<DistrictModel>(district);
    }

    public async Task<List<DistrictModel>> GetAllAsync()
    {
        var districts = await _context.Districts
            .Include(d => d.Region)
            .ToListAsync();
        return _mapper.Map<List<DistrictModel>>(districts);
    }
}