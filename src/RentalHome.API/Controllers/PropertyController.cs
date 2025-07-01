using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.PropertyModel;
using RentalHome.Application.Services.Interfaces;

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var properties = await _propertyService.GetAllAsync();
        return Ok(properties);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var properties = await _propertyService.GetByIdAsync(id);
        if(properties == null) return NotFound();
        return Ok(properties);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]  CreatePropertyModel model)
    {
        var created = await _propertyService.CreateAsync(model);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePropertyModel model)
    {
        var result = await _propertyService.UpdateAsync(id, model);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _propertyService.DeleteAsync(id);
        return result ? NoContent(): NotFound();
    }
}
