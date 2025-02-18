namespace Cinema.Application.DTO.MovieDTOs
{
    public class UpdateMovieDTO
    {
        public string? Title { get; set; } 
        public string? Overview { get; set; }  
        public string? ReleaseDate { get; set; } 
        public double CinemaRating { get; set; }
        public string? PosterPath { get; set; } 
    }
}
