using AuthenticationProject.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;



namespace AuthenticationProject.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize] // NOTE: THIS LINE OF CODE IS NEWLY ADDED
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                UserName = User.Identity?.Name ?? string.Empty // NOTE: THIS LINE OF CODE IS NEWLY ADDED
            })
            .ToArray();
        }
        // NOTE: THIS ENTIRE BLOCK OF CODE IS NEWLY ADDED
        [HttpGet("{date}")]
        [Authorize]
        public WeatherForecast Get(DateTime date)
        {
            var rng = new Random();
            return new WeatherForecast
            {
                Date = date,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                UserName = User.Identity?.Name ?? string.Empty
            };
        }
        // NOTE: END BLOCK
    }
}