using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class Tenant : BaseEntity
{
    public int UserId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public User User { get; set; }
    public ICollection<Booking> Bookings { get; set; }
    public ICollection<Review> PropertyReviews { get; set; }

}
