using Cinema.Domain.Interfaces;

namespace Cinema.Application.Helpers.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IHallRepository Hall { get; }
        IMovieRepository Movie { get; }
        ISessionRepository Session { get; }
        ITicketRepository Ticket { get; }
        IRatingRepository Rating { get; }
        Task CompleteAsync();
    }
}
