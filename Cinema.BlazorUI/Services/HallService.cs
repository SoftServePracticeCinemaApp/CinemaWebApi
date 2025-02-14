using Cinema.BlazorUI.Model;
using Cinema.BlazorUI.Services.Interfaces;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cinema.BlazorUI.Services
{
    public class HallService : IHallService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public HallService(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient.CreateClient("WebApiUrl");
        }

        public async Task<List<FormattedHall>> GetHallsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Hall/formatted");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received JSON: {content}"); 

                var dtos = JsonSerializer.Deserialize<List<HallDto>>(content, _jsonSerializerOptions);
                Console.WriteLine($"Deserialized DTOs: {JsonSerializer.Serialize(dtos, _jsonSerializerOptions)}"); 

                var result = dtos?.Select(dto =>
                {
                    var hall = new FormattedHall
                    {
                        Id = dto.Id,
                        SeatsJson = JsonSerializer.Serialize(dto.Seats)
                    };
                    Console.WriteLine($"Created hall with Id: {hall.Id}"); 
                    return hall;
                }).ToList() ?? new List<FormattedHall>();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetHallsAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}"); 
                return new List<FormattedHall>();
            }
        }
    }

    public class HallDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("seats")]
        public List<List<int>> Seats { get; set; }
    }
}
