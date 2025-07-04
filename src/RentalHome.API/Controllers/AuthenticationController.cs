using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models;
using RentalHome.Application.Services;

namespace RentalHome.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IUserService userService) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser(RegisterUserModel model)
    {
        var result = await userService.RegisterAsync(model.FirstName, model.LastName, model.Email, model.Password,
            model.PhoneNumber, model.UserName);

        if (!result.Succeeded)
            return BadRequest(result);

        return Ok(result);

    }


    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser(LoginUserModel model)
    {
        var result = await userService.LoginAsync(model);

        if (!result.Succeeded)
            return BadRequest(result);

        return Ok(result);
    }


}
