using Microsoft.AspNetCore.Http;
using RentalHome.Application.Models.Photo;

namespace RentalHome.Application.Services;

public interface IPhotoService
{
    Task<IQueryable<PhotoModel>> GetPhotosAsync();
    Task<PhotoModel> GetPhotoAsync(int id);
    Task<ResponsePhotoModel> CreatePhotoAsync(CreatePhotoModel  model);
    Task UploadToFileStorageAsync(IFormFile file);
    Task TransferTempImagesToMinio(int propertyId);
    Task<ResponsePhotoModel> DeletePhotoAsync(int id);
}