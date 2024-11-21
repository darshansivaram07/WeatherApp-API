using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Interface;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAppController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly TokenGenerationService _tokenHelper;

        public WeatherAppController(IWeatherService weatherService, TokenGenerationService tokenHelper)
        {
            _weatherService = weatherService;
            _tokenHelper = tokenHelper;
        }
        [HttpGet("token")]
        public IActionResult GetToken()
        {
            var token = _tokenHelper.GenerateToken();
            return Ok(new { Token = token });
        }
        [Authorize(Roles = "User")]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentWeather(string location)
        {
            var weather = await _weatherService.GetCurrentWeatherAsync(location);
            return Ok(weather);
        }
        [Authorize(Roles = "User")]
        [HttpGet("past7days")]
        public async Task<IActionResult> GetPast7DaysWeather(string location)
        {
            var weather = await _weatherService.GetPast7DaysWeatherAsync(location);
            return Ok(weather);
        }
        [Authorize(Roles = "User")]
        [HttpGet("forecast7days")]
        public async Task<IActionResult> Get7DayForecast(string location)
        {
            var weather = await _weatherService.Get7DayForecastAsync(location);
            return Ok(weather);
        }
    }
}
