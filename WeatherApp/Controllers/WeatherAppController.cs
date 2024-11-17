using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Interface;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAppController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherAppController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentWeather(string location)
        {
            var weather = await _weatherService.GetCurrentWeatherAsync(location);
            return Ok(weather);
        }

        [HttpGet("past7days")]
        public async Task<IActionResult> GetPast7DaysWeather(string location)
        {
            var weather = await _weatherService.GetPast7DaysWeatherAsync(location);
            return Ok(weather);
        }

        [HttpGet("forecast7days")]
        public async Task<IActionResult> Get7DayForecast(string location)
        {
            var weather = await _weatherService.Get7DayForecastAsync(location);
            return Ok(weather);
        }
    }
}
