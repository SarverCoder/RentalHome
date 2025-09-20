using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public  class District : BaseEntity
{
    public string Name { get; set; }
    public int RegionId { get; set; }
    public Region Region { get; set; }

    public ICollection<Property> Properties { get; set; } = new List<Property>();

}
