using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class PropertyType : BaseEntity 
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public string IconClass { get; set; }

    public ICollection<Property> Properties { get; set; } = new List<Property>();
    public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
}
