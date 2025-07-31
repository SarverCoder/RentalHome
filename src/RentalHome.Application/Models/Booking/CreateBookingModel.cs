using RentalHome.Core.Enums;

namespace RentalHome.Application.Models.Booking;

public class CreateBookingModel
{
    public int PropertyId { get; set; }
    public int TenantId { get; set; }
    public int LandlordId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public BookingStatus BookingStatus { get; set; } = BookingStatus.Occupied;
}