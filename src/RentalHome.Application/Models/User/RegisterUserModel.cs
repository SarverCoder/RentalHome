using System.ComponentModel.DataAnnotations;

namespace RentalHome.Application.Models;

public class RegisterUserModel
{
    public string PhoneNumber { get; set; }

    public string Email { get; set; }
 
    public string UserName { get; set; }
  
    public string Password { get; set; }
    public bool isAdminSite { get; set; } = true;

    // Landlord fields
    public bool IsLandlord { get; set; } = false;
    public string? CompanyName { get; set; }
    public string? Bio { get; set; }

}