using CityGuide.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CityGuide.Maui.Services
{
    public class EventApiService
    {
        private readonly HttpClient _httpClient;

        // API'nin adresi (MauiWebApi launchSettings.json: http://localhost:5068)
#if ANDROID
        private const string BaseUrl = "http://10.0.2.2:5068";
#else
        private const string BaseUrl = "http://localhost:5068";
#endif
        public EventApiService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<List<SpecialEvent>> GetEventsAsync()
        {
            // API'ye GET isteği at, JSON'u doğrudan List<SpecialEvent>'e çevir
            var events = await _httpClient.GetFromJsonAsync<List<SpecialEvent>>($"{BaseUrl}/api/events");
            return events ?? new List<SpecialEvent>();
        }
    }
}
