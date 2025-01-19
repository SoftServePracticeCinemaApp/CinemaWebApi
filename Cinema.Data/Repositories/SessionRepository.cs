using Cinema.Business.Entities;
using Cinema.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly CinemaDbContext _context;
    public SessionRepository(CinemaDbContext context)
    {
        _context = context;
    }
    public async Task Add(SessionEntity session)
    {
        if (session == null) throw new ArgumentNullException(nameof(session) + "can't be null");
        await _context.AddAsync(session);
    }

    public async Task Delete(long Id)
    {
        var sessionInDb = await _context.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == Id);

        if (sessionInDb == null) throw new InvalidOperationException($"session with id {Id} doesn't exist");
        await Task.Run(() => _context.Remove(Id));
    }

    public async Task<SessionEntity> Get(long Id)
    {
        var sessionInDb = await _context.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == Id);

        if (sessionInDb == null) throw new ArgumentException($"session with Id {Id} doesn't exist");
        return sessionInDb;
    }

    public async Task<IEnumerable<SessionEntity>> GetAll() => await _context.Sessions.ToListAsync();

    public async Task<IEnumerable<SessionEntity>> GetByDate(DateTime dateTime) => 
        await _context.Sessions
        .AsNoTracking()
        .Where(s => s.Date == dateTime)
        .ToListAsync();

    public async Task Update(long Id, SessionEntity session)
    {
        if(session == null) throw new ArgumentException($"{nameof(session)} can't be null");

        var sessionInDb = await _context.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == Id);

        if(sessionInDb == null) throw new InvalidOperationException($"session with Id {Id} doesn't exist");

        sessionInDb.Date = session.Date;
        sessionInDb.MovieId = session.MovieId;
        sessionInDb.HallId = session.HallId;
    }
    
    public async Task Save() => await _context.SaveChangesAsync();
}
