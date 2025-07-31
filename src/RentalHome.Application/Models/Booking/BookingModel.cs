namespace RentalHome.Application.Models.Booking;

public class BookingModel
{
    public int PropertyId { get; set; }
    public int TenantId { get; set; }
    public int LandlordId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string BookingStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}