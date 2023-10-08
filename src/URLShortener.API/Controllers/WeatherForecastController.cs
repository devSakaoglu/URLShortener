using Microsoft.AspNetCore.Mvc;
using URLShortener.API.Authentication;
using URLShortener.Domain.Entities;
using URLShortener.Domain.Enums;

namespace URLShortener.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly JwtProvider _jwtProvider;

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, JwtProvider jwtProvider)
    {
        _logger = logger;
        _jwtProvider = jwtProvider;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = _jwtProvider.GenerateToken(new AppUser
                {
                    Id = Guid.NewGuid(),
                    UserType = UserType.User
                })
            })
            .ToArray();
    }
}