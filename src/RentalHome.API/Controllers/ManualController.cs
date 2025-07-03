using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.EnumModel;
using RentalHome.Core.Enums;

namespace RentalHome.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManualController : ControllerBase
{
    [HttpGet("enum/select-list")]
    public ActionResult<List<SelectListItemDto>> GetEnumSelectList([FromQuery] string enumName)
    {
        Type? enumType = enumName switch
        {
            "BookingStatus" => typeof(BookingStatus),
            "NotificationType" => typeof(NotificationType),
            "PropertyStatus" => typeof(PropertyStatus),
            "ReviewType" => typeof(ReviewType),
            _ => null
        };

        if (enumType == null || !enumType.IsEnum)
            return BadRequest("Unknown or unsupported enum name");

        var list = Enum.GetValues(enumType)
            .Cast<object>()
            .Select(e => new SelectListItemDto()
            {
                Value = (int)e,
                Text = e.ToString() ?? ""
            }).ToList();

        return Ok(list);

    }
}
