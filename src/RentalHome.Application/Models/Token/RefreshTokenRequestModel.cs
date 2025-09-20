namespace RentalHome.Application.Models.Token;

public class RefreshTokenRequestModel
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}