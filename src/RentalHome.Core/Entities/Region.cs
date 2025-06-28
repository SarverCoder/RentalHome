using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class Region : BaseEntity
{
    public string Name { get; set; }

    public ICollection<District> Districts {  get; set; }
    
}
