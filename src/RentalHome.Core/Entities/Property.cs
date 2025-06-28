using RentalHome.Core.Common;
using RentalHome.Core.Enums;

namespace RentalHome.Core.Entities;

public class Property : BaseEntity
{

    public long LandlordId { get; set; }

    public long PropertypeId { get; set; }

    public long DistrictId { get; set; }

    public District District { get; set; }

    public long RegionId { get; set; }

    public Region Region { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public PropertyStatus PropertyStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool AllowsPets { get; set; }

    public bool IsFurnished { get; set; }

    public long MinRentalPeriod { get; set; }

    public Landlord Landlord { get; set; }

    public PropertyType PropertyType    { get; set; }

}
