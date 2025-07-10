using System.ComponentModel.DataAnnotations;

namespace RentalHome.Application.Models.User;

public class RegisterTenantModel
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

    public decimal Latitude { get; set; } = 0;

    public decimal Longitude { get; set; } = 0;

    public bool isAdminSite { get; set; } = false;
}