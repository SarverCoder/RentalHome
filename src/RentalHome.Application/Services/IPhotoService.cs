using Microsoft.AspNetCore.Http;
using RentalHome.Application.Models.Photo;
using System.Threading.Tasks;

namespace RentalHome.Application.Services;

public interface IPhotoService
{
    Task<IQueryable<PhotoModel>> GetPhotosAsync();
    Task<PhotoModel> GetPhotoAsync(int id);
    Task<ResponsePhotoModel> CreatePhotoAsync(CreatePhotoModel  model);
    Task<string> UploadToFileStorageAsync(IFormFile file);
    Task<Stream> DonwloadImageFromMinio(string phtoUrl);
    Task TransferTempImagesToMinio(int propertyId, IList<string> fileNames);
    Task<ResponsePhotoModel> DeletePhotoAsync(int id);
    string GetMimeType(string fileName);
}