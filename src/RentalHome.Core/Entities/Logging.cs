using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class Logging : BaseEntity
{
    public int UserId { get; set; }              // Landlord UserId
    public string Role { get; set; } = null!;    // "Landlord"
    public string Path { get; set; } = null!;    // /api/property/create
    public string Method { get; set; } = null!;  // POST, PUT, DELETE
    public string Message { get; set; } = null!; // Description of the action
    public string Type { get; set; } = null!;    // Info, Warning, Error
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
