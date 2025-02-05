using Cinema.BlazorUI.Model;

namespace Cinema.BlazorUI.Services.Interfaces;

public interface IMovieService
{
  Task<List<FormattedMovie>> GetMoviesAsync();
}

