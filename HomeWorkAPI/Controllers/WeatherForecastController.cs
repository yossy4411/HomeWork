using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace HomeWorkAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private static readonly string connectionString = "Data Source=34.168.36.70:3306;Initial Catalog=HomeWork;Integrated Security=True;";
        private static readonly SqlConnection connection = new(connectionString);
        private static readonly string[] Summaries = 
            [
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            ];

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            connection.Open();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost(Name = "PostTest")]
        public IActionResult Post(string body)
        {
            _logger.LogInformation($"{body} Recieved (POST)");
            return Ok(body);
        }
    }
}
