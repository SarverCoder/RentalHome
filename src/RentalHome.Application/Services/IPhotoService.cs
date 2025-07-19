using Microsoft.AspNetCore.Http;
using RentalHome.Application.Models.Photo;
using System.Threading.Tasks;

namespace RentalHome.Application.Services;

public interface IPhotoService
{
    //Deal with database
    Task<IQueryable<PhotoModel>> GetPhotosAsync();
    Task<PhotoModel> GetPhotoAsync(int id);
    Task<ResponsePhotoModel> CreatePhotoAsync(CreatePhotoModel  model);
    Task<ResponsePhotoModel> DeletePhotoAsync(string url);
    string GetMimeType(string fileName);

    //Deal with minio
    Task<string> UploadToFileStorageAsync(IFormFile file);
    Task<Stream> DonwloadImageFromMinio(string phtoUrl);
    Task TransferTempImagesToMinio(int propertyId, IList<string> fileNames);

}