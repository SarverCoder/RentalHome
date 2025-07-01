using RentalHome.Application.Models.Booking;
using RentalHome.Core.Entities;

namespace RentalHome.Application.Services;

public interface IBookingService
{
    Task AddAsync(CreateBookingModel createBookingModel);
    Task<bool> UpdateAsync(UpdateBookingModel updateBookingModel);
    Task<bool> DeleteAsync(int id);
    Task<BookingModel> GetByIdAsync(int id);
    Task<List<BookingModel>> GetAllAsync();    
}