using AirlineAPI.Models;
using AirlineAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMessageProducer _messageProducer;
    private static readonly List<Booking> _bookings = new();
    public BookingsController(IMessageProducer messageProducer, ILogger<WeatherForecastController> logger)
    {
        _messageProducer = messageProducer;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult MakeBooking(Booking newBooking)
    {
        if(!ModelState.IsValid)
            return BadRequest();

        _bookings.Add(newBooking);
        _messageProducer.SendingMessage<Booking>(newBooking);
        return Ok();
    }
}