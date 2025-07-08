using Microsoft.AspNetCore.Mvc;
using RentalHome.API.Attributes;
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
        var result = await userService.RegisterAsync( model.Email, model.Password,
            model.PhoneNumber, model.UserName, model.isAdminSite);

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

    [HttpPost("verify-otp")]
    public async Task<ApiResult<string>> VerifyOtpAsync([FromBody] OtpVerificationModel model)
    {
        var result = await userService.VerifyOtpAsync(model);
        return result;
    }

    [Authorize]
    [HttpGet("Get-User-Auth")]
    public async Task<IActionResult> GetUserAuth()
    {
        var result = await userService.GetUserAuth();
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

}
