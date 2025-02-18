using Cinema.BlazorUI.Services.Interfaces;
using Cinema.BlazorUI.Model;
using System.Text.Json;
using RestSharp;
using Cinema.BlazorUI.Model.TMDb;
using Cinema.BlazorUI.Model.DTO;
using System.Net.Http.Json;
using static Cinema.BlazorUI.Components.Admin.CreateSession;

namespace Cinema.BlazorUI.Services;

public class AdminService : IAdminService
{
    private readonly HttpClient _httpClient;
    private readonly string _movieApiKey;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    

    public AdminService(IHttpClientFactory httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient.CreateClient("WebApiUrl");
        _movieApiKey = configuration["MovieApiKey"] ?? "";
    }

    public async Task<FormResult> DeleteMovieAsync(int movieId)
    {
        var response = await _httpClient.DeleteAsync($"api/admin/Movies/Delete/{movieId}");
        return await HandleResponse(response);
    }

    public async Task<FormResult> DeleteSessionAsync(int sessionId)
    {
        var response = await _httpClient.DeleteAsync($"api/admin/Sessions/Delete/{sessionId}");
        return await HandleResponse(response, "Failed to delete session, check session ID");
    }

    public async Task<FormResult> AddTicketsAsync(int sessionId, int numberOfTickets, double price)
    {
        var response = await _httpClient.PostAsJsonAsync("api/admin/Tickets/Add", new
        {
            sessionId = sessionId,
            count = numberOfTickets,
            price = price
        });
        return await HandleResponse(response, "Failed to add tickets, check session ID");
    }

    public async Task<FormResult> CreateSessionAsync(int movieId, List<Components.Admin.CreateSession.SessionInfo> sessions)
    {
        foreach (var session in sessions) 
        {
            var response = await _httpClient.PostAsJsonAsync("api/admin/Sessions/Add", new
            {
                movieId = movieId,
                date = session.Date,
                hallId = session.HallNumber,
                ticketPrice = session.TicketPrice
            });
            var result = await HandleResponse(response, "Failed to create session");
            if (!result.Succeeded)
            {
                return result;
            }
        }
        return new FormResult { Succeeded = true };
    }

    public async Task<FormResult> UpdateSessionAsync(int sessionId, DateTime date, int hallNumber, int ticketPrice)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/admin/Sessions/Update/{sessionId}", new UpdateSessionDTO
        {
            Date = date,
            HallId = hallNumber,
            TicketPrice = ticketPrice
        });
        return await HandleResponse(response, "Failed to update session");
    }

    public async Task<FormResult> UpdateMovieAsync(int movieId, int searchId, double cinemaRating)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/admin/Movies/Update/{movieId}", new
        {
            searchId = searchId,
            cinemaRating = cinemaRating,

        });
        return await HandleResponse(response, "Failed to update movie");
    }

    public async Task<FormResult> DeleteTicketAsync(int ticketId)
    {
        var response = await _httpClient.DeleteAsync($"api/admin/Tickets/Delete/{ticketId}");
        return await HandleResponse(response, "Failed to delete ticket");
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
            catch { 
                errorMessage = "Failed to parse error message";
             }
        }

        return new FormResult
        {
            Succeeded = false,
            ErrorList = new[] { errorMessage }
        };
    }

    public async Task<List<MovieResult>> GetMovieByTitle(string title = "") 
    {
        var options = new RestClientOptions($"https://api.themoviedb.org/3/search/movie?query={title}&include_adult=true&language=en-US&page=1");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_movieApiKey}");
        
        try {
            var response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                var result = JsonSerializer.Deserialize<MovieSearchResponse>(
                    response.Content, 
                    _jsonSerializerOptions
                );
                
                return result?.Results ?? new List<MovieResult>();
            }
            return new List<MovieResult>();
        }
        catch (Exception ex) {
            Console.WriteLine($"Error getting movie: {ex.Message}");
            return new List<MovieResult>();
        }
    }

    public async Task<FromattedSession> GetSessionAsync(int movieId, DateTime date, int hallNumber)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/admin/Sessions/Get?movieId={movieId}&date={date:yyyy-MM-ddTHH:mm:ss}&hallId={hallNumber}");
            if (response.IsSuccessStatusCode)
            {
                var jsonDoc = await response.Content.ReadFromJsonAsync<JsonDocument>(_jsonSerializerOptions);
                var dataElement = jsonDoc.RootElement.GetProperty("data");

                var session = dataElement.Deserialize<FromattedSession>(_jsonSerializerOptions);
                return session;
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting session: {ex.Message}");
            return null;
        }
    }

    Task<List<MovieResult>> IAdminService.GetMovieByTitle(string title)
    {
        return GetMovieByTitle(title);
    }

    public async Task<FormResult> CreateMovieAsync(int movieId)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/admin/Movies/Add", movieId);

        return await HandleResponse(response, "Failed to create movie");
    }

    public async Task<FormResult> UpdateTicketsAsync(int sessionId, int numberOfTickets, double price)
    {
        var response = await _httpClient.PutAsJsonAsync("api/admin/update-tickets", new { sessionId, numberOfTickets, price });
        return await HandleResponse(response, "Failed to update tickets");
    }
}