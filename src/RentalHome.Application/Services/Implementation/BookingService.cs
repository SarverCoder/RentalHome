using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Models.Booking;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services;

public class BookingService(DatabaseContext context, IMapper mapper) : IBookingService
{
    public async Task AddAsync(CreateBookingModel createBookingModel)
    {
        var booking = mapper.Map<Booking>(createBookingModel);
        await context.Bookings.AddAsync(booking);
        await context.SaveChangesAsync();
    }

    public Task<bool> UpdateAsync(UpdateBookingModel updateBookingModel)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var booking = await context.Bookings.FindAsync(id);
        if(booking is null) return false;
        
        context.Bookings.Remove(booking);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<BookingModel> GetByIdAsync(int id)
    {
        var booking = await context.Bookings.FindAsync(id); 
        return mapper.Map<BookingModel>(booking);
    }

    public async Task<List<BookingModel>> GetAllAsync()
    {
        var bookings = await context.Bookings.ToListAsync();
        return mapper.Map<List<BookingModel>>(bookings);
    }
}