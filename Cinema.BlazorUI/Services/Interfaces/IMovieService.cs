using Cinema.BlazorUI.Model;
using Cinema.BlazorUI.Model.TMDb;

namespace Cinema.BlazorUI.Services.Interfaces;

public interface IMovieService
{
  Task<List<FormattedMovie>> GetMoviesAsync();
  Task<FormattedMovie> GetMovieByIdAsync(int id);
  Task<MovieResult> GetMovieDataAsync(int id);
  Task<string> GetMovieTrailerAsync(int id);
  Task<List<string>> GetMovieActorsAsync(int id);
}

