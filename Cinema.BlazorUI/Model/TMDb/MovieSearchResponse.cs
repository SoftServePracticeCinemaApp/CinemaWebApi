using System.Text.Json.Serialization;

namespace Cinema.BlazorUI.Model.TMDb
{
    public class MovieSearchResponse
    {
        public int Page { get; set; }
        public List<MovieResult> Results { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }

    public class MovieResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }
        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }
        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }
        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }
        public bool Adult { get; set; }
        public double Popularity { get; set; }
        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }
    }
} 