using RentalHome.Application.Models;
using RentalHome.Application.Models.Booking;

namespace RentalHome.Application.Services;

public interface IBookingService
{
    Task<ApiResult<string>> AddAsync(CreateBookingModel createBookingModel);
    Task<ApiResult<string>> UpdateAsync(int id,UpdateBookingModel updateBookingModel);
    Task<ApiResult<string>> DeleteAsync(int id);
    Task<BookingModel> GetByIdAsync(int id);
    Task<List<BookingModel>> GetAllAsync();    
}