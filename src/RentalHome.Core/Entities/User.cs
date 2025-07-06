using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class User : BaseEntity
{
 
    public string? Fullname { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public DateTime TokenExpiryTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public Tenant Tenant { get; set; }
    public Landlord Landlord { get; set; }
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
