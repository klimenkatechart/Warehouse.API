using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Domain.Entities;

namespace Warehouse.API.Controllers
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
        private readonly IProductRepository _productRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
           await _productRepository.Insert(new Product() { Name="Cocacold",});
           return Ok();
        }
    }
}