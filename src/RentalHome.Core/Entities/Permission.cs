using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class Permission : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigation property
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}