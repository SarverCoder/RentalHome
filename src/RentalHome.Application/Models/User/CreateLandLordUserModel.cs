namespace RentalHome.Application.Models;

public class CreateLandLordUserModel
{
    public string Email { get; set; } = null!;
    public string Firstname { get; set; } = null!;
    public string Password { get; set; } = string.Empty;
}
