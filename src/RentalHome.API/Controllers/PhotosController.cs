using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.Photo;
using RentalHome.Application.Services;

namespace RentalHome.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotosController(IPhotoService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllPhotos()
    {
        return Ok(await service.GetPhotosAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdPhoto(int id)
    {
        return Ok(service.GetPhotoAsync(id));
    }


    [HttpPost]
    public async Task<IActionResult> CreatePhoto([FromBody] CreatePhotoModel request)
    {
        var res = await service.CreatePhotoAsync(request);

        if (!res.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(res);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteByIdPhoto(int id)
    {
        var res = await service.DeletePhotoAsync(id);

        if (!res.IsSuccess)
        {
            return NotFound();
        }

        return Ok(res);

    }


}
