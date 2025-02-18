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

    public async Task<string> GetMovieTrailerAsync(int id)
    {
        var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{id}/videos?language=en-US");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_movieApiKey}");

        try {
            var response = await client.GetAsync(request);
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var result = JsonSerializer.Deserialize<MovieTrailerResponse>(response.Content, _jsonSerializerOptions);
                if (result?.Results?.Any() == true)
                {
                    var trailer = result.Results.FirstOrDefault(v => v.Type.Equals("Trailer", StringComparison.OrdinalIgnoreCase));
                    if (trailer != null)
                    {
                        // return $"https://www.youtube.com/embed/{trailer.Key}";
                        return $"https://www.youtube.com/watch?v={trailer.Key}";
                    }
                }
                return "";
            }
            return "";
        }
        catch (Exception ex) {
            Console.WriteLine($"Error getting movie trailer: {ex.Message}");
            return "";
        }

    }

    public async Task<List<string>> GetMovieActorsAsync(int id)
    {
        var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{id}/credits?language=en-US");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_movieApiKey}");
    
        try {
            var response = await client.GetAsync(request);
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var result = JsonSerializer.Deserialize<MovieCreditsResponse>(response.Content, _jsonSerializerOptions);

                return result?.Cast?.Select(c => c.Name).ToList() ?? new List<string>();
            }
            return new List<string>();
        }

        catch (Exception ex) {
            Console.WriteLine($"Error getting movie actors: {ex.Message}");
            return new List<string>();
        }
    }

    public async Task<bool> HasUserRatedMovieAsync(int movieId)
    {

        try
        {
            var response = await _httpClient.GetAsync($"api/movie/{movieId}/rate/has-rating");
            return response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking if user rated movie: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> RateMovieAsync(int movieId, int rating)
    {
        try
        {
            if (rating < 1 || rating > 10)
            {
                return false;
            }

            var hasRating = await HasUserRatedMovieAsync(movieId);
            if (hasRating)
            {
                var response = await _httpClient.PutAsJsonAsync($"api/movie/{movieId}/rate", new { rating });
                return response.IsSuccessStatusCode;
            }

            var postResponse = await _httpClient.PostAsJsonAsync($"api/movie/{movieId}/rate", new { rating });
            return postResponse.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error rating movie: {ex.Message}");
            return false;
        }
    }
    public async Task<double?> GetUserRatingAsync(int movieId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/movie/{movieId}/rate");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<double>();
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting user rating: {ex.Message}");
            return null;
        }
    }

}