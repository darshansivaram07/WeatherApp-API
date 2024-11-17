using System.Text.Json;
using WeatherApp.Interface;
using static WeatherApp.Models.DataModel;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        public WeatherService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration["WeatherApiKey"];
        }

        private HttpClient GetWeatherApiClient() => _httpClientFactory.CreateClient("WeatherApi");

        public async Task<WeatherDataModel> GetCurrentWeatherAsync(string location)
        {
            var client = GetWeatherApiClient();
            var response = await client.GetAsync($"{location}/today?unitGroup=metric&key={_apiKey}&include=current&contentType=json");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error fetching current weather data: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<WeatherResponse>(json);

            return new WeatherDataModel
            {
                Date = data.CurrentConditions.Datetime,
                Temperature = Math.Round(data.CurrentConditions.Temp, 1),
                Description = data.CurrentConditions.Conditions,
                Humidity = Math.Round(data.CurrentConditions.Humidity, 1),
                WindSpeed = Math.Round(data.CurrentConditions.Windspeed, 1)
            };


        }

        public async Task<IEnumerable<WeatherDataModel>> GetPast7DaysWeatherAsync(string location)
        {
            var client = GetWeatherApiClient();
            var response = await client.GetAsync($"{location}/last7days?unitGroup=metric&key={_apiKey}&contentType=json");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error fetching past 7 days weather data: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<WeatherResponse>(json);
            var results = new List<WeatherDataModel>();
            foreach (var day in data.Days)
            {
                results.Add(new WeatherDataModel
                {
                    Date = day.Datetime,
                    Temperature = Math.Round(day.Temp,1),
                    Description = day.Conditions,
                    Humidity = Math.Round(day.Humidity, 1),
                    WindSpeed = Math.Round(day.Windspeed, 1)
                });
            }
            return results;
        }

        public async Task<IEnumerable<WeatherDataModel>> Get7DayForecastAsync(string location)
        {
            var client = GetWeatherApiClient();
            var response = await client.GetAsync($"{location}/next7days?unitGroup=metric&key={_apiKey}&include=fcst&contentType=json");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error fetching 7-day forecast: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<WeatherResponse>(json);

            var results = new List<WeatherDataModel>();
            foreach (var day in data.Days)
            {
                results.Add(new WeatherDataModel
                {
                    Date = day.Datetime,
                    Temperature = Math.Round(day.Temp, 1),
                    Description = day.Conditions,
                    Humidity = Math.Round(day.Humidity, 1),
                    WindSpeed = Math.Round(day.Windspeed, 1)
                });
            }
            return results;
        }
    }

}
