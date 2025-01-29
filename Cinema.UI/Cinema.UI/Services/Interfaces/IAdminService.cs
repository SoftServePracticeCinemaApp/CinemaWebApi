using Cinema.UI.Model;

namespace Cinema.UI.Services.Interfaces;

public interface IAdminService
{
  Task<FormResult> DeleteMovieAsync(int movieId);
  Task<FormResult> DeleteSessionAsync(int sessionId);
  Task<FormResult> AddTicketsAsync(int sessionId, int numberOfTickets, double price);
  Task<FormResult> CreateSessionAsync(int movieId, List<DateTime> dates, int hallNumber);
  Task<FormResult> UpdateSessionAsync(int sessionId, DateTime date, int hallNumber);
  Task<FormResult> UpdateMovieAsync(int movieId, string title, string description, string imageUrl, string trailerUrl);
  Task<FormResult> DeleteTicketAsync(int ticketId);
}