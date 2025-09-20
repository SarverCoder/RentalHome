using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class PropertyAmenity : BaseEntity
{
    public int PropertyId { get; set; }
    public Property Property { get; set; }
    public int AmenityId  { get; set; }
    public Amenity Amenity { get; set; }
}
