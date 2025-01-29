using Cinema.UI.Services.Interfaces;
using Cinema.UI.Model;
using System.Text.Json;

namespace Cinema.UI.Services;

public class AdminService : IAdminService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    

    public AdminService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient.CreateClient("WebApiUrl");
    }

    public async Task<FormResult> DeleteMovieAsync(int movieId)
    {
        var response = await _httpClient.DeleteAsync($"api/admin/delete-movie/{movieId}");
        return await HandleResponse(response);
    }

    public async Task<FormResult> DeleteSessionAsync(int sessionId)
    {
        var response = await _httpClient.DeleteAsync($"api/admin/delete-session/{sessionId}");
        return await HandleResponse(response, "Failed to delete session, check session ID");
    }

    public async Task<FormResult> AddTicketsAsync(int sessionId, int numberOfTickets, double price)
    {
        var response = await _httpClient.PostAsJsonAsync("api/admin/add-tickets", new
        {
            sessionId,
            numberOfTickets,
            price
        });
        return await HandleResponse(response, "Failed to add tickets, check session ID");
    }

    public async Task<FormResult> CreateSessionAsync(int movieId, List<DateTime> dates, int hallNumber)
    {
        var response = await _httpClient.PostAsJsonAsync("api/admin/create-session", new
        {
            movieId,
            dates,
            hallNumber
        });
        return await HandleResponse(response, "Failed to create session");
    }

    public async Task<FormResult> UpdateSessionAsync(int sessionId, DateTime date, int hallNumber)
    {
        var response = await _httpClient.PutAsJsonAsync("api/admin/update-session", new
        {
            sessionId,
            date,
            hallNumber
        });
        return await HandleResponse(response, "Failed to update session");
    }

    public async Task<FormResult> UpdateMovieAsync(int movieId, string title, string description, string imageUrl, string trailerUrl)
    {
        var response = await _httpClient.PutAsJsonAsync("api/admin/update-movie", new
        {
            movieId,
            title,
            description,
            imageUrl,
            trailerUrl
        });
        return await HandleResponse(response, "Failed to update movie");
    }

    public async Task<FormResult> DeleteTicketAsync(int ticketId)
    {
        var response = await _httpClient.DeleteAsync($"api/admin/delete-ticket/{ticketId}");
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
}