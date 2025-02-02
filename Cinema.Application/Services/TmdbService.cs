using AutoMapper;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Utils;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cinema.Application.Services
{
    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IMapper _mapper;

        public TmdbService(IMapper mapper, IOptions<TmdbSettings> tmdbSettings, IHttpClientFactory httpClientFactory)
        {
            _apiKey = tmdbSettings.Value.ApiKey;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            _mapper = mapper;
        }

        public async Task<MovieEntity?> GetMovieByIdAsync(int searchId, string language = "en-UA")
        {
            Console.WriteLine($"Fetching movie with ID {searchId} from TMDB...");

            var response = await _httpClient.GetAsync($"movie/{searchId}?api_key={_apiKey}&language={language}");

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error fetching movie: {errorMessage}");
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"TMDB Response: {jsonResponse}");

            var tmdbMovie = JsonSerializer.Deserialize<TmdbMovie>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Console.WriteLine($"TMDB Release Date: {tmdbMovie.ReleaseDate}");
            Console.WriteLine($"TMDB Vote Average: {tmdbMovie.VoteAverage}");
            Console.WriteLine($"TMDB Poster Path: {tmdbMovie.PosterPath}");

            return _mapper.Map<MovieEntity>(tmdbMovie);
        }
    }

    public class TmdbMovie
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }  // Строка, бо може бути пустою

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }
    }


}