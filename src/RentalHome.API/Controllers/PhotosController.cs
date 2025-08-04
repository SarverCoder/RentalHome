using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.Photo;
using RentalHome.Application.Services;
using System.IO;
using System.Net;

namespace RentalHome.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotosController(IPhotoService service,
    IFileStorageService fileStorageService) : ControllerBase
{

    [HttpGet("get-by/{id}")]
    public async Task<IActionResult> GetByIdPhoto(int id)
    {
        return Ok(await service.GetPhotoAsync(id));
    }

    [HttpGet("get-by-url/{url}")]
    public async Task<IActionResult> GetByUrlPhotoFromMinio(string url)
    {
        var filename = WebUtility.UrlDecode(url);
        var result = await service.DonwloadImageFromMinio(filename);
          


        var contentType = service.GetMimeType(filename);

        return File(result, contentType, filename);
        //return new FileStreamResult(result, contentType);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadPhoto(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Fayl tanlanmagan yoki bo'sh.");
        }

        // Fayl nomini noyob qilish uchun Guid va original kengaytmadan foydalanamiz
        
        var result = await service.UploadToFileStorageAsync(file);

        return Ok(result);
    
    }

    [HttpDelete("delete-by/{url}")]
    public async Task<IActionResult> DeleteByUrlPhoto(string url)
    {
        var filename = WebUtility.UrlDecode(url);
        var res = await service.DeletePhotoAsync(filename);

        if (!res.IsSuccess)
        {
            return NotFound();
        }

        return Ok(res);

    }


}
