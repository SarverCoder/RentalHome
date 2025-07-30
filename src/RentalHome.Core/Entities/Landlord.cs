using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class Landlord : BaseEntity
{
    public int UserId { get; set; }
    public string CompanyName { get; set; }
    public string Bio {  get; set; }
    public User User { get; set; }
    public ICollection<Property> Properties { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Booking> Bookings { get; set; }
}
