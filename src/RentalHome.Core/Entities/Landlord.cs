using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class Landlord : BaseEntity
{
    public long UserId { get; set; }

    public string CompanyName { get; set; }

    public string Bio {  get; set; }

    public bool IsVerified { get; set; }

    public User User { get; set; }

    public ICollection<Property> Properties { get; set; }
}
