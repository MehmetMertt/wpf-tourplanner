using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TourPlanner.Domain;

namespace TourPlanner.BL.OpenRouteServiceAPI
{

    public class RouteRequest : IRouteRequest
    {


        [JsonPropertyName("coordinates")]
        public List<List<double>> Coordinates { get; set; }

        [JsonPropertyName("units")]
        public string Units { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("instructions")]
        public bool Instructions { get; set; }
    }

    public class OpenRouteServiceClient : IOpenRouteServiceClient
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RouteRequest));


        private static OpenRouteServiceClient _instance = null;


        private static HttpClient sharedClient = new()
        {
        };

        private string _url = "https://api.openrouteservice.org/v2/directions/";

        private OpenRouteServiceClient()
        {
            sharedClient.BaseAddress = new Uri(_url);

            sharedClient.DefaultRequestHeaders.Add("Accept", "application/json");
            Debug.WriteLine(AppSettingsManager.GetSetting("openroute_key"));
            sharedClient.DefaultRequestHeaders.Add("Authorization", AppSettingsManager.GetSetting("openroute_key"));
        }

        public static OpenRouteServiceClient Instanz
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OpenRouteServiceClient();
                }
                return _instance;
            }
        }



        public async Task<(float Distance, float DurationHours)> GetTimeDistance(VehicleProfile profile, TourModel tour)
        {
            try
            {
                string to = tour.To;
                string from = tour.From;

                GeocodingService geocodingService = new GeocodingService();
                Coordinates? fromCoords = await geocodingService.GetCoordinatesForCity(from);
                Coordinates? toCoords = await geocodingService.GetCoordinatesForCity(to);

                if (fromCoords == null || toCoords == null)
                {
                    if (fromCoords == null)
                    {
                        log.Error($"From Coordinates for {tour.From} are null");
                    }
                    if (toCoords == null)
                    {
                        log.Error($"To Coordinates for {tour.To} are null");

                    }
                    throw new Exception("Failed to retrieve coordinates for one or both locations.");
                }

                var routeRequest = new RouteRequest
                {
                    Coordinates = new List<List<double>>
            {
                new List<double> { fromCoords.Value.Longitude, fromCoords.Value.Latitude },
                new List<double> { toCoords.Value.Longitude, toCoords.Value.Latitude }
            },
                    Units = "km",
                    Language = "en",
                    Instructions = false
                };

                string url = _url + profile.GetDescription();
                string json = JsonSerializer.Serialize(routeRequest);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await sharedClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMsg = await response.Content.ReadAsStringAsync();
                    log.Error($"Http Request to {_url} failed using request body {json} with statuscode: {response.StatusCode} and error message {errorMsg}");
                    throw new HttpRequestException($"API call failed: {response.StatusCode}, {errorMsg}");
                }

                var responseBody = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(responseBody);
                var root = doc.RootElement;

                float distance = root.GetProperty("routes")[0].GetProperty("summary").GetProperty("distance").GetSingle();
                float duration = root.GetProperty("routes")[0].GetProperty("summary").GetProperty("duration").GetSingle();

                return (distance, duration / 3600);
            }
            catch (HttpRequestException ex)
            {
                log.Error($"API {_url} not available.");
                throw new Exception("Route Service is down (contact admin): " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Please check 'To' and 'From' Input", ex);
            }
        }


    }
}
