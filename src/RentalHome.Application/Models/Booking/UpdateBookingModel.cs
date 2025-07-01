using RentalHome.Core.Enums;

namespace RentalHome.Application.Models.Booking;

public class UpdateBookingModel
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public BookingStatus? BookingStatus { get; set; }
}