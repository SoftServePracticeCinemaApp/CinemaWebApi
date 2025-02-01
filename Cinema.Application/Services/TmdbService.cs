using AutoMapper;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Utils;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

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

        public async Task<MovieEntity> GetMovieByIdAsync(int searchId, string language = "en-UA")
        {
            Console.WriteLine($"Fetching movie with ID {searchId} and language {language} from TMDB.");

            var response = await _httpClient.GetAsync($"movie/{searchId}?api_key={_apiKey}&language={language}");

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Failed to fetch movie from TMDB. Status code: {response.StatusCode}, Message: {errorMessage}");
            }

            var tmdbMovie = await response.Content.ReadFromJsonAsync<TmdbMovieResponse>();
            if (tmdbMovie == null)
            {
                throw new InvalidOperationException("Received empty response from TMDB.");
            }

            return _mapper.Map<MovieEntity>(tmdbMovie);
        }
    }

    public class TmdbMovieResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public double? VoteAverage { get; set; } 
        public string? PosterPath { get; set; }
    }
}