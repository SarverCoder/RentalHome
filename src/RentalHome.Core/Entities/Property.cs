using RentalHome.Core.Common;
using RentalHome.Core.Enums;

namespace RentalHome.Core.Entities;

public class Property : BaseEntity
{
    public int LandlordId { get; set; }
    public Landlord Landlord { get; set; }
    public int DistrictId { get; set; }
    public District District { get; set; }
    public int RegionId { get; set; }
    public Region Region { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public PropertyStatus PropertyStatus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } 

    // Navigation Property
    public ICollection<PropertyAmenity> PropertyAmenities { get; set; } = new List<PropertyAmenity>();
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

}
