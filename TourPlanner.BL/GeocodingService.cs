using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace TourPlanner.BL
{
    public class NominatimResult
    {
        [JsonProperty("lat")]
        public string LatitudeString { get; set; }

        [JsonProperty("lon")]
        public string LongitudeString { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
    }

    public struct Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Coordinates(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }
    }

    public class GeocodingService
    {
        private readonly HttpClient _httpClient;
        private const string NominatimBaseUrl = "https://nominatim.openstreetmap.org/search";

        public GeocodingService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:139.0) Gecko/20100101 Firefox/139.0");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
            _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US"));
            _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en", 0.5));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("zstd"));
            _httpClient.DefaultRequestHeaders.Add("Sec-GPC", "1");
            _httpClient.DefaultRequestHeaders.ConnectionClose = false; // "Connection: keep-alive"
            _httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "none");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
            _httpClient.DefaultRequestHeaders.Add("Priority", "u=0, i");
            _httpClient.DefaultRequestHeaders.Add("TE", "trailers");
        }

        public async Task<Coordinates?> GetCoordinatesForCity(string cityName)
        {
            string encodedCityName = Uri.EscapeDataString(cityName);
            string requestUrl = $"{NominatimBaseUrl}?format=json&q={encodedCityName}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<NominatimResult> results = JsonConvert.DeserializeObject<List<NominatimResult>>(jsonResponse);

                if (results != null && results.Any())
                {
                    var firstResult = results.First();

                    if (double.TryParse(firstResult.LatitudeString, NumberStyles.Float, CultureInfo.InvariantCulture, out double lat) &&
                        double.TryParse(firstResult.LongitudeString, NumberStyles.Float, CultureInfo.InvariantCulture, out double lon))
                    {
                        Console.WriteLine($"Found coordinates for {firstResult.DisplayName}: Lat={lat}, Lon={lon}");
                        return new Coordinates(lat, lon);
                    }
                    else
                    {
                        Console.WriteLine($"Could not parse coordinates for '{cityName}' from string values (check format).");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine($"No coordinates found for '{cityName}'.");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                return null;
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return null;
            }
        }
    }
}