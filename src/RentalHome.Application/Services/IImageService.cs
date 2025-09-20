using Microsoft.AspNetCore.Http;
using RentalHome.Core.Entities;


namespace RentalHome.Application.Services;

public interface IImageService
{
    public Task<Photo> UploadImageAsync(IFormFile file);
    public Task<IEnumerable<Photo>> UploadImagesAsync(IEnumerable<IFormFile> files);
    public Task<bool> DeleteImageAsync(long imageId);
}