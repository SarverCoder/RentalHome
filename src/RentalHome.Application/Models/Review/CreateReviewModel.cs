using RentalHome.Core.Enums;

namespace RentalHome.Application.Models.Review;

public class CreateReviewModel
{
    public int BookingId { get; set; }
    public int PropertyId { get; set; }
    public int TargetUserId { get; set; } // Either TenantId or LandlordId
    public int Rating { get; set; }
    public string Comment { get; set; }
    public ReviewType ReviewType { get; set; }
}