using Cinema.Domain.Interfaces;

namespace Cinema.Infrastructure.Utils
{
    interface IUnitOfWork : IDisposable
    {
        IHallRepository HallRepository { get; }
        IMovieRepository MovieRepository { get; }
        ISessionRepository SessionRepository { get; }
        ITicketRepository TicketRepository { get; }
        Task CompleteAsync();
    }
}
