using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.PropertyAmenity;
using RentalHome.Application.Services;

namespace RentalHome.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyAmenitiesController : ControllerBase
{
    private readonly IPropertyAmenityService _propertyAmenityService;

    public PropertyAmenitiesController(IPropertyAmenityService propertyAmenityService)
    {
        _propertyAmenityService = propertyAmenityService;
    }

    // GET: api/PropertyAmenities
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _propertyAmenityService.GetAllAsync();
        return Ok(result);
    }

    // GET: api/PropertyAmenities/5/3
    [HttpGet("{propertyId:int}/{amenityId:int}")]
    public async Task<IActionResult> Get(int propertyId, int amenityId)
    {
        var result = await _propertyAmenityService.GetAsync(propertyId, amenityId);
        if (result == null)
            return NotFound("Amenity for this property not found");

        return Ok(result);
    }

    // POST: api/PropertyAmenities
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PropertyAmenityModel dto)
    {
        var response = await _propertyAmenityService.CreateAsync(dto);
        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    // DELETE: api/PropertyAmenities/5/3
    [HttpDelete("{propertyId:int}/{amenityId:int}")]
    public async Task<IActionResult> Delete(int propertyId, int amenityId)
    {
        var response = await _propertyAmenityService.DeleteAsync(propertyId, amenityId);
        if (!response.IsSuccess)
            return NotFound(response);

        return Ok(response);
    }
}
