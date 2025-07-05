namespace RentalHome.Application.Models.PropertyModel;

public class CreatePropertyModel
{
    public int LandlordId { get; set; }
    public int PropertyTypeId { get; set; }
    public int DistrictId { get; set; }
    public int RegionId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public bool AllowsPets { get; set; }
    public bool IsFurnished { get; set; }
}
