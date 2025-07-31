using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Models;
using RentalHome.Application.Models.Booking;
using RentalHome.Core.Entities;
using RentalHome.Core.Exceptions;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services;

public class BookingService(DatabaseContext context, IMapper mapper) : IBookingService
{
    public async Task<ApiResult<string>> AddAsync(CreateBookingModel createBookingModel)
    {
        var booking = mapper.Map<Booking>(createBookingModel);

        await context.Bookings.AddAsync(booking);
        await context.SaveChangesAsync();

        return ApiResult<string>.Success("Booking taratildi.");
    }

    public async Task<ApiResult<string>> UpdateAsync(int id, UpdateBookingModel updateBookingModel)
    {
        var booking = await context.Bookings.FirstOrDefaultAsync(x => x.Id == id);

        if (booking == null) 
            return ApiResult<string>.Failure(new List<string>(){"Booking topilmadi"});

        mapper.Map(booking, updateBookingModel);
        await context.SaveChangesAsync();

        return ApiResult<string>.Success("Booking yangilandi");

    }

    public async Task<ApiResult<string>> DeleteAsync(int id)
    {
        var booking = await context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
        if(booking is null) 
            return ApiResult<string>.Failure([$"{id} ga tegishli Booking topilmadi"]);
        
        context.Bookings.Remove(booking);
        await context.SaveChangesAsync();

        return ApiResult<string>.Success("Booking ochirildi");
    }

    public async Task<BookingModel> GetByIdAsync(int id)
    {
        var booking = await context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
        if (booking is null)
            throw new NotFoundException("Booking not found");

        return mapper.Map<BookingModel>(booking);
    }

    public async Task<List<BookingModel>> GetAllAsync()
    {
        var bookings = await context.Bookings.ToListAsync();
        return mapper.Map<List<BookingModel>>(bookings);
    }
}