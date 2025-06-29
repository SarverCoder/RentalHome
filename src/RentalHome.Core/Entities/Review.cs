using RentalHome.Core.Common;
using RentalHome.Core.Enums;

namespace RentalHome.Core.Entities;

public class Review : BaseEntity
{
    public int BookingId { get; set; }
    public int PropertyId { get; set; }
    public int TenantId { get; set; }
    public int LandlordId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public  ReviewType ReviewType  { get; set; }


    public Booking Booking { get; set; }
    public Property Property { get; set; }
    public Tenant Tenant { get; set; }
    public Landlord Landlord { get; set; }
}

