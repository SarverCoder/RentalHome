namespace RentalHome.Application.Models.Photo;

public class CreatePhotoModel
{
    public int PropertyId { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; } = false;
    public int Order { get; set; }
}