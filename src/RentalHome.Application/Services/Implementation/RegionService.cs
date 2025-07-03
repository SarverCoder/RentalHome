using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Models.Region;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services.Implementation;


public class RegionService : IRegionService
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public RegionService(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RegionResponseModel> AddAsync(CreateRegionModel createDto)
    {
        try
        {
            // Check if region already exists
            if (await _context.Regions.AnyAsync(r => r.Name == createDto.Name))
            {
                return new RegionResponseModel()
                {
                    IsSuccess = false,
                    Message = "Region with this name already exists"
                };
            }

            var region = _mapper.Map<Region>(createDto);
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();

            return new RegionResponseModel
            {
                IsSuccess = true,
                Message = "Region created successfully"
            };
        }
        catch (Exception ex)
        {
            return new RegionResponseModel
            {
                IsSuccess = false,
                Message = $"Failed to create region: {ex.Message}"
            };
        }
    }

    public async Task<RegionResponseModel> UpdateAsync(UpdateRegionModel updateDto, int id)
    {
        try
        {
            var region = await _context.Regions.FindAsync(id);
            if (region == null)
            {
                return new RegionResponseModel
                {
                    IsSuccess = false,
                    Message = "Region not found"
                };
            }

            if (await _context.Regions.AnyAsync(r => r.Name == updateDto.Name && r.Id != id))
            {
                return new RegionResponseModel
                {
                    IsSuccess = false,
                    Message = "Another region with this name already exists"
                };
            }

            _mapper.Map(updateDto, region);
            _context.Regions.Update(region);
            await _context.SaveChangesAsync();

            return new RegionResponseModel
            {
                IsSuccess = true,
                Message = "Region updated successfully"
            };
        }
        catch (Exception ex)
        {
            return new RegionResponseModel
            {
                IsSuccess = false,
                Message = $"Failed to update region: {ex.Message}"
            };
        }
    }

    public async Task<RegionResponseModel> DeleteAsync(int id)
    {
        try
        {
            var region = await _context.Districts.FindAsync(id);
            if (region == null)
            {
                return new RegionResponseModel()
                {
                    IsSuccess = false,
                    Message = "Region not found"
                };
            }

            _context.Districts.Remove(region);
            await _context.SaveChangesAsync();

            return new RegionResponseModel
            {
                IsSuccess = true,
                Message = "Region deleted successfully"
            };
        }
        catch (Exception ex)
        {
            return new RegionResponseModel
            {
                IsSuccess = false,
                Message = $"Region to delete district: {ex.Message}"
            };
        }
    }

    public async Task<RegionModel> GetByIdAsync(int id)
    {

        var region = await _context.Regions
            .Include(r => r.Districts)
            .FirstOrDefaultAsync(r => r.Id == id);

        return _mapper.Map<RegionModel>(region);
    }

    public async Task<List<RegionModel>> GetAllAsync()
    {
        var regions = await _context.Regions.ToListAsync();

        return _mapper.Map<List<RegionModel>>(regions);
    }


}