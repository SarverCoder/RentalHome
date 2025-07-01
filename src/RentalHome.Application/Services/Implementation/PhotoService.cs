using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Models.Photo;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services;

public class PhotoService(DatabaseContext context, IMapper mapper) : IPhotoService
{
    public Task<IQueryable<PhotoModel>> GetPhotosAsync()
    {
        var photos = context.Photos.AsQueryable().AsNoTracking();

        return Task.FromResult(mapper.ProjectTo<PhotoModel>(photos));
    }

    public async Task<PhotoModel> GetPhotoAsync(int id)
    {
        var photo = await context.Photos.FirstOrDefaultAsync(k => k.Id == id);

        if (photo is null)
            throw new Exception("Not found");

        return mapper.Map<PhotoModel>(photo);

    }

    public async Task<ResponsePhotoModel> CreatePhotoAsync(CreatePhotoModel model)
    {
        var photoModel = mapper.Map<Photo>(model);

        await context.AddAsync(photoModel);
        await context.SaveChangesAsync();

        return new ResponsePhotoModel()
        {
            IsSuccess = true,
            Status = "Successfully created"

        };


    }

    public Task<ResponsePhotoModel> UpdatePhotoAsync(UpdatePhotoModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponsePhotoModel> DeletePhotoAsync(int id)
    {
        var photo = await context.Photos.FirstOrDefaultAsync(key => key.Id == id);

        if (photo is null)
            throw new Exception("Not found");

        context.Remove(photo);
        await context.SaveChangesAsync();

        return new ResponsePhotoModel()
        {
            IsSuccess = true,
            Status = "Deleted Successfully"
        };

    }
}