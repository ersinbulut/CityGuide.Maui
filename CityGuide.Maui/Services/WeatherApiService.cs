using CityGuide.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CityGuide.Maui.Services
{
    public class WeatherApiService
    {
        private const string ApiKey = "630ce9cc86msh271c60cffe62d5ep1b514djsn0fe292593744";
        private const string ApiHost = "yahoo-weather5.p.rapidapi.com";

        public async Task<WeatherResponse?> GetMilanoWeatherAsync()
        {
            using var client = new HttpClient();

            string url = $"https://{ApiHost}/weather?location=milano&format=json&u=c";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Headers =
                {
                    { "x-rapidapi-key", ApiKey },
                    { "x-rapidapi-host", ApiHost },
                },
            };

            try
            {
                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<WeatherResponse>(body);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
