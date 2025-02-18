using Cinema.Domain.Interfaces;
using Cinema.Application.Helpers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Cinema.Infrastructure.Repositories;

namespace Cinema.Infrastructure.Utils
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaDbContext _context;
        public IHallRepository Hall { get; }
        public IMovieRepository Movie { get; }
        public ISessionRepository Session { get; }
        public ITicketRepository Ticket { get; }
        public IRatingRepository Rating { get; private set; }

        public UnitOfWork(CinemaDbContext context,
                          IHallRepository hallRepository,
                          IMovieRepository movieRepository,
                          ISessionRepository sessionRepository,
                          ITicketRepository ticketRepository)
        {
            _context = context;
            Hall = hallRepository;
            Movie = movieRepository;
            Session = sessionRepository;
            Ticket = ticketRepository;
            Rating = new RatingRepository(_context);
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
