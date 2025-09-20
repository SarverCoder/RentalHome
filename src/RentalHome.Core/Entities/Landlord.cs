using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class Landlord : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public string? CompanyName { get; set; }
    public string? Bio {  get; set; }


    public ICollection<Property> Properties { get; set; } = new List<Property>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
