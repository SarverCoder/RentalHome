using RentalHome.Application.Models.Amenity;
using RentalHome.Core.Enums;

namespace RentalHome.Application.Models.PropertyModel;

public class PropertyModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public PropertyStatus PropertyStatus { get; set; }


    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Linked Entities (Region, District, Landlord)
    public string RegionName { get; set; } = string.Empty;

    public string DistrictName { get; set; } = string.Empty;

    public string LandlordName { get; set; } = string.Empty;

    // Photo URLs (if you need image support in frontend)
    public List<ImageGetDto> PhotoUrls { get; set; } = new();

    // Amenities (e.g. "WiFi", "AC")
    public List<AmenityModel> Amenities { get; set; } = new();

    // Average Rating (optional)
    public double? AverageRating { get; set; }
}

public class ImageGetDto
{
    public int Id { get; set; }
    public string Image { get; set; }
    public string Url { get; set; }
}
