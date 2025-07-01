namespace RentalHome.Application.Models.Landlord;

public class LandlordModel
{
    public int UserId { get; set; }

    public string CompanyName { get; set; }

    public string Bio {  get; set; }

    public bool IsVerified { get; set; }
}