using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.Amenity;
using RentalHome.Application.Services;

namespace RentalHome.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AmenityController : Controller
{
    private readonly IAmenityService _amenityService;
    public AmenityController(IAmenityService amenityService)
    {
        _amenityService = amenityService;
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var amenities = await _amenityService.GetAllAsync();
        return Ok(amenities);
    }
    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var amenity = await _amenityService.GetByIdAsync(id);
        if (amenity == null)
        {
            return NotFound();
        }
        return Ok(amenity);
    }
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateAmenityModel dto)
    {
        if (dto == null)
        {
            return BadRequest("Amenity data is required.");
        }
       
        return Ok(await _amenityService.CreateAmenityAsync(dto));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateAmenityModel dto, int id)
    {
        if (dto == null || id <= 0)
        {
            return BadRequest("Valid amenity data is required.");
        }
        var result = await _amenityService.UpdateAmenityAsync(dto, id);
        if (!result)
        {
            return NotFound("Amenity not found.");
        }
        return NoContent();
    }
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Valid amenity ID is required.");
        }
        var result = await _amenityService.DeleteAmenityAsync(id);
        if (!result)
        {
            return NotFound("Amenity not found.");
        }
        return NoContent();
    }       
}
