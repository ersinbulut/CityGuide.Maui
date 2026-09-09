using System.Text.Json.Serialization;

namespace CityGuide.Maui.Models
{
    public class WeatherAtmosphere
    {
        [JsonPropertyName("humidity")]

        public int Humidity { get; set; }
    }
}
