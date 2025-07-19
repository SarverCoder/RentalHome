using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel;
using RentalHome.Application.Common;
using RentalHome.Application.Models.Photo;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services;

public class PhotoService : IPhotoService
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;
    private readonly MinioSettings _minioSettings;

    public PhotoService(
        DatabaseContext context,
        IMapper mapper,
        IFileStorageService fileStorageService,
        IOptions<MinioSettings> minioSettings)
    {
        _context = context;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
        _minioSettings = minioSettings.Value;
    }
    public Task<IQueryable<PhotoModel>> GetPhotosAsync()
    {
        var photos = _context.Photos.AsQueryable().AsNoTracking();

        return Task.FromResult(_mapper.ProjectTo<PhotoModel>(photos));
    }

    public async Task<PhotoModel> GetPhotoAsync(int id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(k => k.Id == id);

        if (photo is null)
            throw new Exception("Not found");

        return _mapper.Map<PhotoModel>(photo);

    }
    public async Task<ResponsePhotoModel> CreatePhotoAsync(CreatePhotoModel model)
    {
        var photoModel = _mapper.Map<Photo>(model);

        await _context.AddAsync(photoModel);
        await _context.SaveChangesAsync();

        return new ResponsePhotoModel()
        {
            IsSuccess = true,
            Status = "Successfully created"

        };
    }

    public async Task UploadToFileStorageAsync(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName);
        var objectName = $"{Guid.NewGuid()}{fileExtension}"; // Minio'da saqlanadigan fayl nomi

        var pa = "C:\\Users\\User\\temp-upload";
        Directory.CreateDirectory(pa);

        var tempPath = Path.Combine(pa, objectName);


        using (var stream = new FileStream(tempPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
    }

    public async Task TransferTempImagesToMinio(int propertyId)
    {
        var bucket = _minioSettings.BucketName;

       
        foreach (var tempImageName in Directory.GetFiles("C:\\Users\\User\\temp-upload"))
        {
            var fileName = Path.GetFileName(tempImageName);
            var objectKey = $"property/{propertyId}/{fileName}";

            var contentType = GetMimeType(fileName);

            using (var fileStream = File.OpenRead(tempImageName))
            using (var memoryStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0; // Reset position after copying
                await _fileStorageService.UploadFileAsync(bucket, objectKey, memoryStream, contentType);

            }

            await _context.Photos.AddAsync(new Photo()
            {
                
                PropertyId = propertyId,
                Url = objectKey
            });
            await _context.SaveChangesAsync();

            // Delete temp file
            File.Delete(tempImageName);
        }
    }
    private string GetMimeType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(fileName, out string contentType))
        {
            contentType = "application/octet-stream"; // Default if unknown
        }
        return contentType;
    }



    public async Task<ResponsePhotoModel> DeletePhotoAsync(int id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(key => key.Id == id);

        if (photo is null)
            throw new Exception("Not found");

        _context.Remove(photo);
        await _context.SaveChangesAsync();

        return new ResponsePhotoModel()
        {
            IsSuccess = true,
            Status = "Deleted Successfully"
        };

    }
}