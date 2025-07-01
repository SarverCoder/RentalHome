using Microsoft.AspNetCore.Mvc;
using RentalHome.Application.Models.Booking;
using RentalHome.Application.Services;

namespace RentalHome.API.Controllers;

[Route("api/[controller]")]
public class BookingController(IBookingService service) : Controller
{
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var booking = await service.GetByIdAsync(id);
        
        return Ok(booking);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var bookings =  await service.GetAllAsync();
        return Ok(bookings);
    }

    [HttpPost]
    public async Task<IActionResult>Create([FromBody] CreateBookingModel bookingModel)
    {
        await service.AddAsync(bookingModel);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] int id)
    {
        var result = await service.DeleteAsync(id);
        
        return Ok(result ? "deleted" : "not deleted");
    }
}