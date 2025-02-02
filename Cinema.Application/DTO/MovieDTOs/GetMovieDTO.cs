namespace Cinema.Application.DTO.MovieDTOs
{
    public class GetMovieDTO
    {
        public int Id { get; set; }
        public int SearchId { get; set; }
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public string? ReleaseDate { get; set; }
        public double CinemaRating { get; set; }
        public string? PosterPath { get; set; }
    }
}
