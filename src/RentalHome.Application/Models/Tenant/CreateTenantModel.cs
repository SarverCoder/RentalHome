namespace RentalHome.Application.Models.Tenant;

public class CreateTenantModel
{
    public int UserId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}
