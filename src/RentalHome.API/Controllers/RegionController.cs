using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.Region;
using RentalHome.Application.Services;

namespace RentalHome.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RegionController(IRegionService service) : ControllerBase
{
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var regions = await service.GetAllAsync();

        return Ok(regions);
    }

    [HttpGet("get-by/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var region = await service.GetByIdAsync(id);
        
        return Ok(region);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateRegionModel region)
    {
        var result = await service.AddAsync(region);
        
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateRegionModel region,[FromRoute] int id)
    {
        var result = await service.UpdateAsync(region, id);
        
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await service.DeleteAsync(id);
        
        return Ok(result);
    }
    
}