using RentalHome.Core.Common;
using RentalHome.Core.Enums;

namespace RentalHome.Core.Entities;

public class Booking : BaseEntity
{
    public int PropertyId { get; set; }
    public int TenantId { get; set; }
    public int LandlordId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public BookingStatus BookingStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public Property Property { get; set; }
    public Tenant Tenant { get; set; }
    public Landlord Landlord { get; set; }
    public Review Review { get; set; }

}

