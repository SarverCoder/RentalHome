using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class PermissionGroup : BaseEntity
{
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}