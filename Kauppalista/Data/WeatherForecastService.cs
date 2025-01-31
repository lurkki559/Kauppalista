namespace Kauppalista.Data;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

public class WeatherForecastService
{
    private readonly HttpClient _httpClient;

    public WeatherForecastService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherForecast?> GetWeatherForecastAsync(string location)
    {
        var apiKey = "f20f05441d5cf56a23a51ed7b352ebcd";
        var url = $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={apiKey}&units=metric";

        var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponse>(url);
        if (response == null) return null;

        return new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = (int)response.Main.Temp,
            Location = response.Name,
            Summary = response.Weather[0].Description
        };
    }
        private class OpenWeatherResponse
        {
            public Main Main { get; set; }
            public Weather[] Weather { get; set; }
            public string Name { get; set; }
        }
        private class Main
        {
            public float Temp { get; set; }
        }
        private class Weather
        {
            public string Description { get; set; }
        }
}
