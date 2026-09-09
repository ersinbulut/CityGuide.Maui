using System.Text.Json.Serialization;

namespace CityGuide.Maui.Models
{
    public class CurrentObservation
    {
        [JsonPropertyName("condition")]
        public WeatherCondition Condition { get; set; } = new();

        [JsonPropertyName("atmosphere")]
        public WeatherAtmosphere Atmosphere { get; set; } = new();
    }
}
