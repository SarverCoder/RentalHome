using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.District;
using RentalHome.Application.Services;

namespace RentalHome.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DistrictController(IDistrictService service) : ControllerBase
{

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var districts = await service.GetAllAsync();
        return Ok(districts);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var district = await service.GetByIdAsync(id);
        return Ok(district);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateDistrictModel model)
    {
        var result = await service.AddAsync(model);
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromBody]UpdateDistrictModel model, [FromRoute]int id)
    {
        var result = await service.UpdateAsync(model,id);
        return Ok(result);
    }
    
    [HttpDelete("delete-by-id/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await service.DeleteAsync(id);
        return Ok(result);
    }
    
    
}