using System.ComponentModel.DataAnnotations;

namespace RentalHome.Application.Models;

public class RegisterUserModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    
}