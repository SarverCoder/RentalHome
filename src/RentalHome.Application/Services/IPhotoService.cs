using RentalHome.Application.Models.Photo;

namespace RentalHome.Application.Services;

public interface IPhotoService
{
    Task<IQueryable<PhotoModel>> GetPhotosAsync();
    Task<PhotoModel> GetPhotoAsync(int id);
    Task<ResponsePhotoModel> CreatePhotoAsync(CreatePhotoModel  model);
    Task<ResponsePhotoModel> UpdatePhotoAsync(UpdatePhotoModel model);
    Task<ResponsePhotoModel> DeletePhotoAsync(int id);
}