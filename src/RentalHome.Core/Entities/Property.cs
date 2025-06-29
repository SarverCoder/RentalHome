using RentalHome.Core.Common;
using RentalHome.Core.Enums;

namespace RentalHome.Core.Entities;

public class Property : BaseEntity
{

    public int LandlordId { get; set; }

    public int PropertyTypeId { get; set; }

    public int DistrictId { get; set; }

    public District District { get; set; }

    public int RegionId { get; set; }

    public Region Region { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public PropertyStatus PropertyStatus { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; }

    public bool AllowsPets { get; set; }

    public bool IsFurnished { get; set; }

    public int MinRentalPeriod { get; set; }

    public Landlord Landlord { get; set; }
    public PropertyType PropertyType { get; set; }
    public ICollection<PropertyAmenity> PropertyAmenities { get; set; }
    public ICollection<Photo> Photos { get; set; }
    public ICollection<Booking> Bookings { get; set; }
    public ICollection<Review> Reviews { get; set; }

}
