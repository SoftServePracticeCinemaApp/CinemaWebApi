using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly CinemaDbContext _context;
    public SessionRepository(CinemaDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(SessionEntity session)
    {
        if (session == null) throw new ArgumentNullException(nameof(session) + "can't be null");
        await _context.AddAsync(session);
    }

    public async Task DeleteByIdAsync(long Id)
    {
        var sessionInDb = await _context.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == Id);

        if (sessionInDb == null) throw new InvalidOperationException($"session with id {Id} doesn't exist");
        await Task.Run(() => _context.Remove(Id));
    }

    public async Task<SessionEntity> GetByIdAsync(long Id)
    {
        var sessionInDb = await _context.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == Id);

        if (sessionInDb == null) throw new ArgumentException($"session with Id {Id} doesn't exist");
        return sessionInDb;
    }

    public async Task<IEnumerable<SessionEntity>> GetAllAsync() => await _context.Sessions.ToListAsync();

    public async Task<IEnumerable<SessionEntity>> GetByDateAsync(DateTime dateTime) =>
        await _context.Sessions
        .AsNoTracking()
        .Where(s => s.Date == dateTime)
        .ToListAsync();

    public async Task UpdateAsync(long Id, SessionEntity session)
    {
        if (session == null) throw new ArgumentException($"{nameof(session)} can't be null");

        var sessionInDb = await _context.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == Id);

        if (sessionInDb == null) throw new InvalidOperationException($"session with Id {Id} doesn't exist");

        sessionInDb.Date = session.Date;
        sessionInDb.MovieId = session.MovieId;
        sessionInDb.HallId = session.HallId;
    }
    public async Task<IEnumerable<SessionEntity>> GetByMovieIdAsync(long movieId)
    {
        return await _context.Sessions
            .Where(s => s.MovieId == movieId)
            .Include(s => s.Hall)
            .ToListAsync();
    }
}
