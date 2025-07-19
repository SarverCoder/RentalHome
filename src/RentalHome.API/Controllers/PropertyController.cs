using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.PropertyModel;
using RentalHome.Application.Services;

namespace RentalHome.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var properties = await _propertyService.GetAllAsync();
        return Ok(properties);
    }

    [HttpGet("get-by/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var property = await _propertyService.GetByIdAsync(id);
        if (property == null)
            return NotFound(new { Message = "Property not found" });

        return Ok(property);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreatePropertyModel model)
    {
        var result = await _propertyService.CreateAsync(model);

        

        return Ok(result);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update([FromBody] UpdatePropertyModel model, [FromRoute] int id)
    {
        var result = await _propertyService.UpdateAsync(model, id);
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _propertyService.DeleteAsync(id);
        return Ok(result);
    }
}
