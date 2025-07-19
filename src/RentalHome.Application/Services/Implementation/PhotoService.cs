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

    public async Task<string> UploadToFileStorageAsync(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName);
        var objectName = $"{Guid.NewGuid()}{fileExtension}"; // Minio'da saqlanadigan fayl nomi

        var path = "wwwroot/temp-upload";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var tempPath = Path.Combine(path, objectName);


        using (var stream = new FileStream(tempPath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        return objectName;
    }

    public async Task TransferTempImagesToMinio(int propertyId, IList<string> fileNames)
    {
        var bucket = _minioSettings.BucketName;

        var files = Directory.GetFiles("wwwroot/temp-upload");
        for (int i = 0; i < files.Length; i++)
        {
            var fileName = Path.GetFileName(files[i]);


            var contentType = GetMimeType(fileName);

            for (int j = 0; j < fileNames.Count; j++)
            {
                if (fileName == fileNames[j])
                {

                    var objectKey = $"property/{propertyId}/{fileName}";
                    using (var fileStream = File.OpenRead(files[i]))
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


                    // Delete temp file
                    File.Delete(files[i]);

                    continue;
                }
            }


        }

        await _context.SaveChangesAsync();
    }
    public async Task<Stream> DonwloadImageFromMinio(string phtoUrl)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(x => x.Url == phtoUrl);

        var stream = await _fileStorageService.DownloadFileAsync(_minioSettings.BucketName, phtoUrl);

        return stream;
    }
    public string GetMimeType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(fileName, out string contentType))
        {
            contentType = "application/octet-stream"; // Default if unknown
        }
        return contentType;
    }



    public async Task<ResponsePhotoModel> DeletePhotoAsync(string url)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(key => key.Url == url);

        if (photo is null)
            throw new Exception("Not found");

        if (! await _fileStorageService.FileExistsAsync(_minioSettings.BucketName, url))
        {
            return new ResponsePhotoModel()
            {
                IsSuccess = false,
                Status = "There is no photo with name"
            };
        }
        await _fileStorageService.RemoveFileAsync(_minioSettings.BucketName, url);

        _context.Remove(photo);
        await _context.SaveChangesAsync();

        return new ResponsePhotoModel()
        {
            IsSuccess = true,
            Status = "Deleted Successfully"
        };

    }
}