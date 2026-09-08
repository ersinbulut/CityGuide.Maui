using CityGuide.Maui.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace CityGuide.Maui.Services
{
    public class EventApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5068";

        public EventApiService()
        {
            _httpClient=new HttpClient();
        }
        public async Task<List<SpecialEvent>> GetEventAsync()
        {
            //Api ye get isteği at json u doğrudan list<specialEvent> olarak deserialize et
            var events = await _httpClient.GetFromJsonAsync<List<SpecialEvent>>
                ($"{BaseUrl}/api/events");
            return events ?? new List<SpecialEvent>();
        }
    }
}
