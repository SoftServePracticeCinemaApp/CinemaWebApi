using Cinema.Domain.Interfaces;
using Cinema.Application.Interfaces;

namespace Cinema.Infrastructure.Utils
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaDbContext _context;
        public IHallRepository HallRepository { get; }
        public IMovieRepository MovieRepository { get; }
        public ISessionRepository SessionRepository { get; }
        public ITicketRepository TicketRepository { get; }

        public UnitOfWork(CinemaDbContext context,
                          IHallRepository hallRepository,
                          IMovieRepository movieRepository,
                          ISessionRepository sessionRepository,
                          ITicketRepository ticketRepository)
        {
            _context = context;
            HallRepository = hallRepository;
            MovieRepository = movieRepository;
            SessionRepository = sessionRepository;
            TicketRepository = ticketRepository;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
