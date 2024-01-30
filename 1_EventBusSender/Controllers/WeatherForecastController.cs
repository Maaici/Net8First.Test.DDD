using Microsoft.AspNetCore.Mvc;
using Zack.EventBus;

namespace _1_EventBusSender.Controllers
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
        private readonly IEventBus _event;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IEventBus ievent)
        {
            _logger = logger;
            _event = ievent;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _event.Publish("OrderCreated", new dataModel { name= "maaici" , age = 31 ,tel = "18852993969"});
            _event.Publish("OrderComplete", new dataModel { name= "xiaomage" , age = 31 ,tel = "18001558581"});

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
    class dataModel {
        public string name { get; set; }
        public int age { get; set; }
        public string tel { get; set; }
    }
}
