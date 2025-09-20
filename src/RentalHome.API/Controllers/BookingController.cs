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
    public async Task<IActionResult> Create([FromBody] CreateBookingModel bookingModel)
    {
        var res = await service.AddAsync(bookingModel);
        return Ok(res);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatedBooking(int id, [FromBody] UpdateBookingModel model)
    {
        var result = await service.UpdateAsync(id, model);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await service.DeleteAsync(id);

        if (!result.Succeeded)
            return BadRequest(result);
        
        return Ok(result);
    }
}