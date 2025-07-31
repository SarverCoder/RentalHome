using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.Review;

namespace RentalHome.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{

    [HttpPost("CreateReview")]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewModel model)
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReviewById(int id)
    {
        return Ok();
    }

    [HttpGet("Property/{propId}")]
    public async Task<IActionResult> GetAllReviewsForProperty(int propId)
    {
        return Ok();
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllReviewsForUSer(int userId)
    {
        return Ok();
    }
}
