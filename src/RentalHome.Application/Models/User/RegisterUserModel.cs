using System.ComponentModel.DataAnnotations;

namespace RentalHome.Application.Models;

public class RegisterUserModel
{
   

    public string PhoneNumber { get; set; }

    public string Email { get; set; }
 
    public string UserName { get; set; }
  
    public string Password { get; set; }
    public bool isAdminSite { get; set; } = true;

}