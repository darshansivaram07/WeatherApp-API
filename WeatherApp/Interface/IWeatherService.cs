using static WeatherApp.Models.DataModel;

namespace WeatherApp.Interface
{
    public interface IWeatherService
    {
        Task<WeatherDataModel> GetCurrentWeatherAsync(string location);
        Task<IEnumerable<WeatherDataModel>> GetPast7DaysWeatherAsync(string location);
        Task<IEnumerable<WeatherDataModel>> Get7DayForecastAsync(string location);
    }
}
