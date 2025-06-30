using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using TourPlanner.Domain;
using log4net;
using System.Text.Json;

namespace TourPlanner.BL.OpenWeatherMapAPI
{
    public class OpenWeatherServiceClient
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OpenWeatherServiceClient));
        private static readonly HttpClient httpClient = new();
        private readonly string _apiKey;
        private readonly string _baseUrl = "https://api.openweathermap.org/data/2.5/forecast";

        private static OpenWeatherServiceClient _instance;
        public static OpenWeatherServiceClient Instance => _instance ??= new OpenWeatherServiceClient();

        private OpenWeatherServiceClient()
        {
            _apiKey = AppSettingsManager.GetSetting("openweather_key"); // thats the thing you get from the app.config
        }

        public async Task<string> GetForecastSummary(string city, string date)
        {
            try
            {
                if (!DateTime.TryParseExact(date, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    return "Invalid date format. Use DD.MM.YYYY";
                }

                string url = $"{_baseUrl}?q={Uri.EscapeDataString(city)}&units=metric&appid={_apiKey}";

                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                var list = doc.RootElement.GetProperty("list");

                foreach (var item in list.EnumerateArray()) // search for weather for the specified day
                {
                    var dtTxt = item.GetProperty("dt_txt").GetString();
                    var forecastTime = DateTime.Parse(dtTxt);

                    if (forecastTime.Date == parsedDate.Date)
                    {
                        var main = item.GetProperty("main");
                        var weather = item.GetProperty("weather")[0];
                        float temp = main.GetProperty("temp").GetSingle();
                        string desc = weather.GetProperty("description").GetString();

                        // return $"{forecastTime:dd.MM. HH:mm}: {desc}, {temp}°C";
                        return $"{desc}, {temp}°C";
                    }
                }

                return "No weather data available for selected date.";
            }
            catch (Exception ex)
            {
                log.Error("Weather forecast failed", ex);
                return "Weather forecast unavailable.";
            }
        }
    }
}
