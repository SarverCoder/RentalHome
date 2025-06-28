using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class Tenant : BaseEntity
{
    public long UserId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public long PreferredPropertyType { get; set; }

    public User User { get; set; }

}
