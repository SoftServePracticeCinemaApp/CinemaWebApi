using Cinema.Domain.Interfaces;

namespace Cinema.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IHallRepository Hall { get; }
        IMovieRepository Movie{ get; }
        ISessionRepository Session{ get; }
        ITicketRepository Ticket{ get; }
        Task CompleteAsync();
    }
}
