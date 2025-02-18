using Cinema.BlazorUI.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace Cinema.BlazorUI.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public AnalyticsService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WebApiUrl");
        }

        public async Task<double> GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            var response = await _httpClient.GetAsync($"api/analytics/total-revenue?start={startDate:yyyy-MM-dd}&end={endDate:yyyy-MM-dd}");
            return await HandleResponse<double>(response);
        }

        public async Task<List<(string MovieTitle, int TicketsSold)>> GetTopMovies(DateTime startDate, DateTime endDate)
        {
            var response = await _httpClient.GetAsync($"api/analytics/top-movies?start={startDate:yyyy-MM-dd}&end={endDate:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            if (doc.RootElement.TryGetProperty("data", out var dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                return dataElement.EnumerateArray()
                    .Select(item => (
                        item.GetProperty("movieTitle").GetString()!,
                        item.GetProperty("ticketsSold").GetInt32()
                    ))
                    .ToList();
            }

            throw new InvalidOperationException("API response does not contain valid 'data' field.");
        }


        public async Task<List<(DateTime Date, double Revenue)>> GetRevenueByPeriod(DateTime startDate, DateTime endDate)
        {
            var response = await _httpClient.GetAsync($"api/analytics/revenue-by-period?start={startDate:yyyy-MM-dd}&end={endDate:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            if (doc.RootElement.TryGetProperty("data", out var dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                return dataElement.EnumerateArray()
                    .Select(item => (
                        item.GetProperty("date").GetDateTime(),
                        item.GetProperty("revenue").GetDouble()
                    ))
                    .ToList();
            }

            throw new InvalidOperationException("API response does not contain valid 'data' field.");
        }


        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            if (doc.RootElement.TryGetProperty("data", out var dataElement))
            {
                return dataElement.Deserialize<T>(_jsonSerializerOptions)!;
            }

            throw new InvalidOperationException("API response does not contain 'data' field.");
        }


    }
}