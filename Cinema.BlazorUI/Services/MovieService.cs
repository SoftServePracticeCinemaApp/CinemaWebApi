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
    private readonly string _movieApiKey;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    

    public MovieService(IHttpClientFactory httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient.CreateClient("WebApiUrl");
        _movieApiKey = configuration["MovieApiKey"] ?? "";
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

    public async Task<FormattedMovie> GetMovieByIdAsync(int id)
    {
        try {
            var response = await _httpClient.GetAsync($"api/movie/formatted/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<FormattedMovie>(content, _jsonSerializerOptions) ?? new FormattedMovie();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetMovieByIdAsync: {ex.Message}");
            return new FormattedMovie();
        }
    }

    public async Task<MovieResult> GetMovieDataAsync(int id)
    {
        var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{id}?language=en-US");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_movieApiKey}");
        
        try {
            var response = await client.GetAsync(request);
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var result = JsonSerializer.Deserialize<MovieResult>(
                    response.Content, 
                    _jsonSerializerOptions
                );
                
                return result ?? new MovieResult();
            }
            return new MovieResult();
        }
        catch (Exception ex) {
            Console.WriteLine($"Error getting movie: {ex.Message}");
            return new MovieResult();
        }
    }
}