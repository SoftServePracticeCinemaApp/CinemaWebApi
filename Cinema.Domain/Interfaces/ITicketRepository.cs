using Cinema.Domain.Entities;

namespace Cinema.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task AddAsync(TicketEntity ticket);
        Task UpdateAsync(int Id, TicketEntity ticketEntity);
        Task DeleteAsync(int Id);
        Task<IEnumerable<TicketEntity>> GetByUserIdAsync(string Id);
        Task<IEnumerable<TicketEntity>> GetAllAsync();
        Task<TicketEntity> GetByIdAsync(int Id);
        Task<IEnumerable<TicketEntity>> GetByMovieIdAsync(int movieId);
        Task<IEnumerable<TicketEntity>> GetBySessionIdAsync(int sessionId);
        Task<IEnumerable<TicketEntity>> GetTicketsForHallAsync(int hallId);
        Task UpdateTicketBookStatus(long ticketId, bool isBooked);
    }
}
