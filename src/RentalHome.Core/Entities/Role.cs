using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}