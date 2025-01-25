using Cinema.Domain.Entities;

namespace Cinema.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task Add(TicketEntity ticket);
        Task Update(long Id, TicketEntity ticketEntity);
        Task Remove(long Id);
        Task<IEnumerable<TicketEntity>> GetByUserId(string Id);
        Task<IEnumerable<TicketEntity>> GetAll();
        Task<TicketEntity> GetById(long Id);
        Task<IEnumerable<TicketEntity>> GetByMovieId(int movieId);
        Task Save();
    }
}
