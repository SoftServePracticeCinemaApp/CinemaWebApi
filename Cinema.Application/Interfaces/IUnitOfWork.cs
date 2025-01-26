using Cinema.Domain.Interfaces;

namespace Cinema.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IHallRepository HallRepository { get; }
        IMovieRepository MovieRepository { get; }
        ISessionRepository SessionRepository { get; }
        ITicketRepository TicketRepository { get; }
        Task CompleteAsync();
    }
}
