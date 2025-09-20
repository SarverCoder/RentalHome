using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.PropertyModel;
using RentalHome.Application.Services;

namespace RentalHome.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public PropertyController(IPropertyService propertyService, IHttpContextAccessor httpContextAccessor)
    {
        _propertyService = propertyService;
        _httpContextAccessor = httpContextAccessor;
    }

    private string GetBaseUrl()
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null)
            return string.Empty;

        return $"{request.Scheme}://{request.Host}";
    }

    private void SetImagesBaseUrl(PropertyModel property)
    {
        if (property?.PhotoUrls == null)
            return;

        var baseUrl = GetBaseUrl();

        foreach (var image in property.PhotoUrls)
        {
            image.Url = $"{baseUrl}/images/{image.Image}";
        }
    }

    private void SetImagesBaseUrl(IEnumerable<PropertyModel> property)
    {
        if (property == null)
            return;

        foreach (var product in property)
            SetImagesBaseUrl(product);
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var properties = await _propertyService.GetAllAsync();
        SetImagesBaseUrl(properties);
        return Ok(properties);
    }

    [HttpGet("get-by/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var property = await _propertyService.GetByIdAsync(id);
        if (property == null)
            return NotFound(new { Message = "Property not found" });
        SetImagesBaseUrl(property);
        return Ok(property);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreatePropertyModel model)
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
