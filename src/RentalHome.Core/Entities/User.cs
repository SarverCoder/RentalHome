using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; }
}
