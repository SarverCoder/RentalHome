using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.Landlord;
using RentalHome.Application.Services;

namespace RentalHome.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LandlordController(ILandlordService service) : ControllerBase
{
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var landlord = await service.GetByIdAsync(id);
        return Ok(landlord);
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var landlords =  await service.GetAllAsync();
        return Ok(landlords);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateLandlordModel landlord)
    {
        await service.AddAsync(landlord);
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await service.DeleteAsync(id);
        
        return Ok(result ?  "deleted" : "not deleted");
    }
    
}