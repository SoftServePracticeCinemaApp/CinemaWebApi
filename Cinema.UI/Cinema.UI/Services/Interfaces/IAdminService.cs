using Cinema.UI.Model;
using Cinema.UI.Model.TMDb;

namespace Cinema.UI.Services.Interfaces;

public interface IAdminService
{
  Task<FormResult> DeleteMovieAsync(int movieId);
  Task<FormResult> DeleteSessionAsync(int sessionId);
  Task<FormResult> AddTicketsAsync(int sessionId, int numberOfTickets, double price);
  Task<FormResult> CreateSessionAsync(int movieId, List<DateTime> dates, int hallNumber);
  Task<FormResult> UpdateSessionAsync(int sessionId, DateTime date, int hallNumber);
  Task<FormResult> UpdateMovieAsync(int movieId, int searchId, double cinemaRating);
  Task<FormResult> DeleteTicketAsync(int ticketId);
  Task<List<MovieResult>> GetMovieByTitle(string title = "");
  Task<FormResult> CreateMovieAsync(int movieId);
  Task<FormResult> UpdateTicketsAsync(int sessionId, int numberOfTickets, double price);
}
