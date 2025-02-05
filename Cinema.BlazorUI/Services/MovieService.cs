using Cinema.BlazorUI.Services.Interfaces;
using Cinema.BlazorUI.Model;
using System.Text.Json;
using RestSharp;
using Cinema.BlazorUI.Model.TMDb;
using System.Net.Http.Json;

namespace Cinema.BlazorUI.Services;

public class MovieService : IMovieService
{
    private readonly HttpClient _httpClient;
    // private readonly string _movieApiKey;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    

    public MovieService(IHttpClientFactory httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient.CreateClient("WebApiUrl");
        // _movieApiKey = configuration["MovieApiKey"] ?? "";
    }

    public async Task<List<FormattedMovie>> GetMoviesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/movie/formatted");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<FormattedMovie>>(content, _jsonSerializerOptions);
            return result ?? new List<FormattedMovie>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetMoviesAsync: {ex.Message}");
            return new List<FormattedMovie>();
        }
    }
}