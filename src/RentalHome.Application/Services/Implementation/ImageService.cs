using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RentalHome.Core.Entities;
using RentalHome.Core.Exceptions;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services.Implementation;

public class ImageService : IImageService
{
    private readonly DatabaseContext _context;

    public ImageService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Photo> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ValidationException("Invalid image file.");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Path.GetRandomFileName()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var image = new Photo()
        {
            Image = fileName,
            Url = $"/images/{fileName}"
        };


        await _context.Photos.AddAsync(image);

        return image;

    }

    public async Task<IEnumerable<Photo>> UploadImagesAsync(IEnumerable<IFormFile> files)
    {
        var uploadedImages = new List<Photo>();

        foreach (var file in files)
        {
            var image = await UploadImageAsync(file);
            uploadedImages.Add(image);
        }

        return uploadedImages;
    }

    public async Task<bool> DeleteImageAsync(long imageId)
    {
        var image = await _context.Photos.FirstOrDefaultAsync(x => x.Id == imageId);
        if (image == null)
            throw new NotFoundException("Image not found.");

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.Url.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        _context.Remove(image);

        return true;
    }
}