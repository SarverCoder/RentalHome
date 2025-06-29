using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public  class Amenity:BaseEntity
{
    public string Name { get; set; }
    public string IconClass { get; set; }

    public ICollection<PropertyAmenity> PropertyAmenities { get; set; }
}
