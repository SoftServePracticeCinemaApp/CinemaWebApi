using Cinema.BlazorUI.Model;
using Cinema.BlazorUI.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace Cinema.BlazorUI.Services
{
    public class TicketService : ITicketService
    {
        private readonly HttpClient _httpClient;
        private readonly string _movieApiKey;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public TicketService(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient.CreateClient("WebApiUrl");
            _movieApiKey = configuration["MovieApiKey"] ?? "";
        }

        public async Task<FormResult> BookTicketAsync(int ticketId)
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync($"/api/ticket/book/{ticketId}");
                return await HandleResponse(response);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error in BooTicketAsync: {ex.Message}");
                return await HandleResponse(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                });
            }
        }

        public async Task<List<Ticket>> GetBySessionIdAsync(int sessionId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ticket/session/{sessionId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                
                var baseResponse = JsonSerializer.Deserialize<JsonElement>(content);
                var ticketsJson = baseResponse.GetProperty("data").GetRawText();
                
                var result = JsonSerializer.Deserialize<List<Ticket>>(ticketsJson, _jsonSerializerOptions);
                return result ?? new List<Ticket>();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in GetTicketsBySessionAsync: {ex.Message}");
                return new List<Ticket>();
            }
        }

        private async Task<FormResult> HandleResponse(HttpResponseMessage response, string defaultErrorMessage = "Operation failed")
        {
            if (response.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }

            var errorMessage = defaultErrorMessage;
            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                try
                {
                    var error = await response.Content.ReadFromJsonAsync<FormResult>(_jsonSerializerOptions);
                    if (error?.ErrorList?.Any() == true)
                    {
                        return error;
                    }
                }
                catch
                {
                    errorMessage = "Failed to parse error message";
                }
            }

            return new FormResult
            {
                Succeeded = false,
                ErrorList = new[] { errorMessage }
            };
        }
    }
}
