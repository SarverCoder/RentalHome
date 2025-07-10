using System.ComponentModel.DataAnnotations;

namespace RentalHome.Application.Models.User;

public class RegisterLandlordModel
{
    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string CompanyName { get; set; }

    public string Bio { get; set; } = "";

    public bool isAdminSite { get; set; } = false;
}